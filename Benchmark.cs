
using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Logging;

[MemoryDiagnoser]
[Orderer(BenchmarkDotNet.Order.SummaryOrderPolicy.FastestToSlowest)]
[RankColumn]
public class Benchmark{

    private static readonly ChatCompletionRequest request = new ChatCompletionRequest
    {
        Temperature = 0,
        TopP = 1,
        FrequencyPenalty = 0,
        PresencePenalty = 0,
        Messages = new List<Message> 
        { 
            new Message
            {
                Content = "Hello World!"
            }
        },
    };
    private const string deploymentId = "DeploymentId";
    private const string apiVersion = "2024-02-01";
    private const string  userId = "userId";
    private static readonly UsageLogger usageLogger= new UsageLogger();

    [Benchmark(Baseline = true)]
    public void LogAnonymousObject()
    {
        usageLogger.GetChatCompletionsBad(request, deploymentId, apiVersion, userId);
    }

    [Benchmark]
    public void LogStructuredLogging()
    {
        usageLogger.GetChatCompletionsGood(request, deploymentId, apiVersion, userId);
    }

}