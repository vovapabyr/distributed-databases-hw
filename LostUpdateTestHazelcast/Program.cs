using Hazelcast;
using Hazelcast.DistributedObjects;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

const string UserMapName = "users-map";
const string UserMapKey = "user-1";

await using var client = await HazelcastClientFactory.StartNewClientAsync(BuilHazelcastOptions());
var logger = client.Options.LoggerFactory.CreateLogger<Program>();

await RunTest(client, logger);

#region tests

async Task LostUpdateTest(IHMap<string, int> map) 
{
    var value = await map.GetAsync(UserMapKey);
    await map.SetAsync(UserMapKey, value + 1);
}

#endregion

#region helpers

async Task RunTest(IHazelcastClient client, ILogger<Program> logger) 
{
    var testName = args[0];
    await using var map = await client.GetMapAsync<string, int>(UserMapName);
    logger.LogInformation($"Starting: {testName}");
    Stopwatch stopwatch = new Stopwatch();
    stopwatch.Start();
    
    IEnumerable<Task> tasks;
    switch (testName)
    {
        case "LostUpdate":
            tasks = RunTasks(async () => await LostUpdateTest(map));
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
        yield return Task.Run(async () =>
        {
            for (int j = 0; j < 10_000; j++)
            {
                await testFunc();
            }
        });
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