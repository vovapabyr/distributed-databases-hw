using Npgsql;
using System.Diagnostics;

var connString = "Server=localhost:5432;Database=postgres;User Id=postgres;Password=postgres;";

//await RunTestWithExecutionTimeLogged("Read-Modify-Write: Lost Update Test", LostUpdate);

//await RunTestWithExecutionTimeLogged("In-place Update Test", InPlaceUpdate);

//await RunTestWithExecutionTimeLogged("Row-level Locking Update Test", RowLockUpdate);

//await RunTestWithExecutionTimeLogged("Optimistic Concurrency Control Update Test", OptimisticConcurrencyUpdate);

//await RunTestWithExecutionTimeLogged("Optimistic Concurrency Control Update Test", OptimisticConcurrencyUpdate, 2, 50_000);

//Automatically deletecting lost updates with the help of isolations level
await RunTestWithExecutionTimeLogged("Automatic Lost Update Deletection Test", AutomaticLostUpdateDetection);

Console.ReadLine();

void LostUpdate(NpgsqlConnection conn) 
{
    int counter;
    using (var cmdSelect = new NpgsqlCommand("SELECT Counter FROM user_counter WHERE User_Id = 1", conn))
        counter = (int)cmdSelect.ExecuteScalar();

    counter = counter + 1;

    using var cmdUpdate = new NpgsqlCommand($"UPDATE user_counter SET Counter = {counter} WHERE User_Id = 1", conn);
    cmdUpdate.ExecuteNonQuery();
}

void InPlaceUpdate(NpgsqlConnection conn)
{
    using var cmdUpdate = new NpgsqlCommand("UPDATE user_counter SET Counter = Counter + 1 WHERE User_Id = 1", conn);
    cmdUpdate.ExecuteNonQuery();
}

void RowLockUpdate(NpgsqlConnection conn)
{
    // The weakest isolation level explicitly used to demonstrate that FOR UPDATE fixes lost update, not isolation level.
    int counter;
    using var tx = conn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
    using (var cmdSelect = new NpgsqlCommand("SELECT Counter FROM user_counter WHERE User_Id = 1 FOR UPDATE", conn))
        counter = (int)cmdSelect.ExecuteScalar();

    counter = counter + 1;

    using var cmdUpdate = new NpgsqlCommand($"UPDATE user_counter SET Counter = {counter} WHERE User_Id = 1", conn);
    cmdUpdate.ExecuteNonQuery();
    tx.Commit();
}

void OptimisticConcurrencyUpdate(NpgsqlConnection conn)
{
    while (true) 
    {
        int counter, version;
        using (var cmdSelect = new NpgsqlCommand("SELECT Counter, Version FROM user_counter WHERE User_Id = 1", conn))
        using (var reader = cmdSelect.ExecuteReader()) 
        {
            reader.Read();
            counter = reader.GetInt32(0);
            version = reader.GetInt32(1);
        }

        counter = counter + 1;

        using var cmdUpdate = new NpgsqlCommand($"UPDATE user_counter SET Counter = { counter }, Version = { version + 1 } WHERE User_Id = 1 AND Version = { version }", conn);
        if (cmdUpdate.ExecuteNonQuery() > 0)
            break;
    }
}

void AutomaticLostUpdateDetection(NpgsqlConnection conn)
{
    int counter;
    using var tx = conn.BeginTransaction(System.Data.IsolationLevel.RepeatableRead);
    using (var cmdSelect = new NpgsqlCommand("SELECT Counter FROM user_counter WHERE User_Id = 1", conn))
        counter = (int)cmdSelect.ExecuteScalar();

    counter = counter + 1;

    using var cmdUpdate = new NpgsqlCommand($"UPDATE user_counter SET Counter = {counter} WHERE User_Id = 1", conn);
    cmdUpdate.ExecuteNonQuery();
    tx.Commit();
}

#region helpers

async Task RunTestWithExecutionTimeLogged(string testName, Action<NpgsqlConnection> testFunc, int tasksCount = 10, int iterationsPerTaskCount = 10_000)
{
    Console.WriteLine($"Starting: {testName}");
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    var tasks = RunTasks(testFunc, tasksCount, iterationsPerTaskCount);

    await Task.WhenAll(tasks);

    stopwatch.Stop();
    Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

    using var conn = new NpgsqlConnection(connString);
    conn.Open();
    using (var cmdSelect = new NpgsqlCommand("SELECT Counter FROM user_counter WHERE User_Id = 1", conn))
        Console.WriteLine($"Counter: {(int)cmdSelect.ExecuteScalar()}");
    
    using var cmdReset = new NpgsqlCommand("UPDATE user_counter SET Counter = 0, Version = 0 WHERE User_Id = 1", conn);
    if(cmdReset.ExecuteNonQuery() > 0)
        Console.WriteLine("Counter and Version are reset to 0");

    Console.WriteLine();
}

IEnumerable<Task> RunTasks(Action<NpgsqlConnection> testFunc, int tasksCount, int iterationsPerTaskCount)
{
    for (int i = 0; i < tasksCount; i++)
        yield return Task.Run(() =>
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                for (int j = 0; j < iterationsPerTaskCount; j++)
                    try
                    {
                        testFunc(conn);
                    }
                    catch
                    {
                        // Retry same update in case of AutomaticLostUpdateDetection.
                        j--;
                    }
            }
        });
}

#endregion