using Npgsql;
using System.Diagnostics;

var connString = "Server=localhost:5432;Database=postgres;User Id=postgres;Password=postgres;";

//await RunTestWithExecutionTimeLogged("Read-Modify-Write: Lost Update Test", LostUpdate);

//await RunTestWithExecutionTimeLogged("In-place Update Test", InPlaceUpdate);

await RunTestWithExecutionTimeLogged("Row-lovel Locking Update Test", RowLockUpdate);

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

#region helpers

async Task RunTestWithExecutionTimeLogged(string testName, Action<NpgsqlConnection> testFunc)
{
    Console.WriteLine($"Starting: {testName}");
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();

    var tasks = RunTasks(testFunc);

    await Task.WhenAll(tasks);

    stopwatch.Stop();
    Console.WriteLine($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");

    using var conn = new NpgsqlConnection(connString);
    conn.Open();
    Console.WriteLine($"Counter: {GetCounter(conn)}");
    
    if(UpdateCounter(conn, 0) > 0)
        Console.WriteLine("Counter is reset to 0");

    Console.WriteLine();
}

IEnumerable<Task> RunTasks(Action<NpgsqlConnection> testFunc)
{
    // Run 10 tasks, each of which executes test function 10_000 times.
    for (int i = 0; i < 10; i++)
        yield return Task.Run(() =>
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                for (int j = 0; j < 10_000; j++)
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