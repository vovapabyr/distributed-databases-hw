using Hazelcast;
using Hazelcast.DistributedObjects;
using Microsoft.Extensions.Logging;

await using var client = await HazelcastClientFactory.StartNewClientAsync(BuilHazelcastOptions());
var logger = client.Options.LoggerFactory.CreateLogger<Program>();

await RunTest(client, logger);

#region tests

async Task LostUpdateTest(IHMap<string, int> map) 
{
    for (int i = 0; i < 10_000; i++)
    {
        var value = await map.GetAsync("user-1");
        await map.SetAsync("user-1", value + 1);
    }
}

#endregion

#region helpers

async Task RunTest(IHazelcastClient client, ILogger<Program> logger) 
{
    var testName = args[0];
    logger.LogInformation($"Start: {testName}");
    await using var map = await client.GetMapAsync<string, int>("users-map");
    switch (testName) 
    {
        case "LostUpdate":
            await LostUpdateTest(map);
            break;
        default:
            throw new NotSupportedException(testName);
    }

    logger.LogInformation($"Ended: {testName}");
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