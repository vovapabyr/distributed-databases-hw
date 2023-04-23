using Hazelcast;
using Hazelcast.Core;
using Hazelcast.DistributedObjects;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

const string UserMapName = "users-map";
const string UserMapKey = "user-1";

await RunTest();

#region tests

async Task LostUpdateTest(IHMap<string, int> map) 
{
    var value = await map.GetAsync(UserMapKey);
    await map.SetAsync(UserMapKey, value + 1);
}

async Task PesimisticLockingUpdateTest(IHMap<string, int> map)
{
    await Task.Delay(10);
    await map.LockAsync(UserMapKey);

    try
    {
        var value = await map.GetAsync(UserMapKey);
        await map.SetAsync(UserMapKey, value + 1);
    }
    finally
    {
        await map.UnlockAsync(UserMapKey);
    }
}

async Task OptimisticLockingUpdateTest(IHMap<string, int> map)
{
    while (true) 
    {
        var oldValue = await map.GetAsync(UserMapKey);
        var newValue = oldValue + 1;
        if (await map.ReplaceAsync(UserMapKey, oldValue, newValue))
            break;
    }
}

#endregion

#region helpers

async Task RunTest() 
{
    await using var client = await HazelcastClientFactory.StartNewClientAsync(BuilHazelcastOptions());
    var logger = client.Options.LoggerFactory.CreateLogger<Program>();
    await using var map = await client.GetMapAsync<string, int>(UserMapName);

    var testName = args[0];
    logger.LogInformation($"Starting: {testName}");
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    
    IEnumerable<Task> tasks;
    switch (testName)
    {
        case "LostUpdate":
            tasks = RunTasks(() => LostUpdateTest(map));
            break;
        case "PesimisticLocking":
            tasks = RunTasks(() => PesimisticLockingUpdateTest(map));
            break;
        case "OptimisticLocking":
            await map.SetAsync(UserMapKey, 0);
            tasks = RunTasks(() => OptimisticLockingUpdateTest(map));
            break;
        default:
            throw new NotSupportedException(testName);
    }

    await Task.WhenAll(tasks);

    stopwatch.Stop();
    logger.LogInformation($"Execution Time: {stopwatch.ElapsedMilliseconds} ms");
    var value = await map.GetAsync(UserMapKey);
    logger.LogInformation($"Counter: {value}");

    await map.SetAsync(UserMapKey, 0);
    logger.LogInformation($"Reset counter to 0");
}

IEnumerable<Task> RunTasks(Func<Task> testFunc)
{
    for (int i = 0; i < 10; i++)
    {
        using (var context = AsyncContext.New())
            yield return Task.Run(async () =>
            {
                for (int j = 0; j < 10_000; j++)
                {
                    await testFunc();
                }
            });
    }
}

#endregion

HazelcastOptions BuilHazelcastOptions() 
{
    var options = new HazelcastOptionsBuilder().WithLoggerFactory(conf => LoggerFactory.Create(builder => builder.AddConsole())).Build();

    options.Networking.Addresses.Add("lostupdatetesthazelcast-hazelcast-1:5701");
    options.Networking.Addresses.Add("lostupdatetesthazelcast-hazelcast-2:5701");
    options.Networking.Addresses.Add("lostupdatetesthazelcast-hazelcast-3:5701");
    options.ClusterName = "dev";
    options.ClientName = "Lost Update Test Client";

    return options;
}