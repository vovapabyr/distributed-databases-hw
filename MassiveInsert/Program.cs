using Npgsql;
using System.Diagnostics;

var connString = "Server=localhost:5432;Database=postgres;User Id=postgres;Password=postgres;";

await RunTestWithExecutionTimeLogged("Read-Modify-Write: Lost Update Test", LostUpdate);

//await RunTestWithExecutionTimeLogged("In-place Update Test", InPlaceUpdate);

//await RunTestWithExecutionTimeLogged("Row-level Locking Update Test", RowLockUpdate);

// Optimistic concurrency is super slow with 10 threads and 10_000 iterations each ~ 40min
//await RunTestWithExecutionTimeLogged("Optimistic Concurrency Control Update Test", OptimisticConcurrencyUpdate);

// Optimistic concurrency with 10 threads with 50_000 iterations per each is a way faster ~  4min. So, we can see the prove that Optimistic Concurrency has performance degradation with big number of concurrent updates.
//await RunTestWithExecutionTimeLogged("Optimistic Concurrency Control Update Test", OptimisticConcurrencyUpdate, 2, 50_000);

Console.ReadLine();

void LostUpdate(NpgsqlConnection conn) 
{
    var counter = GetCounter(conn);
    counter = counter + 1;
    UpdateCounter(conn, counter);
}

void InPlaceUpdate(NpgsqlConnection conn)
{
    using var cmdUpdate = new NpgsqlCommand("UPDATE user_counter SET Counter = Counter + 1 WHERE User_Id = 1", conn);
    cmdUpdate.ExecuteNonQuery();
}

void RowLockUpdate(NpgsqlConnection conn)
{
    // The weakest isolation level explicitly used to demonstrate that FOR UPDATE fixes lost update, not isolation level.
    using var tx = conn.BeginTransaction(System.Data.IsolationLevel.ReadUncommitted);
    var counter = GetCounter(conn, true);
    counter = counter + 1;
    UpdateCounter(conn, counter);
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
    Console.WriteLine($"Counter: {GetCounter(conn)}");
    
    using var cmdReset = new NpgsqlCommand("UPDATE user_counter SET Counter = 0, Version = 0 WHERE User_Id = 1", conn);
    if(cmdReset.ExecuteNonQuery() > 0)
        Console.WriteLine("Counter and Version are reset to 0");

    Console.WriteLine();
}

IEnumerable<Task> RunTasks(Action<NpgsqlConnection> testFunc, int tasksCount, int iterationsPerTaskCount)
{
    // Run 10 tasks, each of which executes test function 10_000 times.
    for (int i = 0; i < tasksCount; i++)
        yield return Task.Run(() =>
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                for (int j = 0; j < iterationsPerTaskCount; j++)
                        testFunc(conn);
            }
        });
}

int GetCounter(NpgsqlConnection conn, bool rowLock = false)
{
    var select = "SELECT Counter FROM user_counter WHERE User_Id = 1";
    select = rowLock ? $"{select} FOR UPDATE" : select;
    using var cmdSelect = new NpgsqlCommand(select, conn);
    return (int)cmdSelect.ExecuteScalar();
}

int UpdateCounter(NpgsqlConnection conn, int counter) 
{
    using var cmdUpdate = new NpgsqlCommand("UPDATE user_counter SET Counter = @counter WHERE User_Id = 1", conn);
    cmdUpdate.Parameters.AddWithValue("counter", counter);
    return cmdUpdate.ExecuteNonQuery();
}

#endregion