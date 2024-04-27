using Microsoft.Extensions.Logging;

public class UsageLogger
{
    ILogger<Benchmark> _logger = new LoggerFactory().CreateLogger<Benchmark>();

    public Result<ChatCompletionResponse> GetChatCompletions(ChatCompletionRequest request, string deploymentId, string apiVersion)
    {
        return new ChatCompletionResponse{};
    }

    private Result<IEnumerable<object>> ParseOpenAiResponse(string openAiResponse)
    {
        return new List<object>();
    }

    public async Task<Result<IEnumerable<object>>> GetChatCompletionsBad(ChatCompletionRequest request, string deploymentId, string apiVersion, string? userId = null)
    {
        var result = GetChatCompletions(request, deploymentId, apiVersion);

        if (result.IsSuccess)
        {
            var usage = result.Value.Usage;
            if (userId != null)
            {
                _logger.LogInformation("Token usage {Tokens}", new
                {
                    UserId = userId,
                    Usage = new
                    {
                        PromptTokens = usage.PromptTokens,
                        CompletionTokens = usage.CompletionTokens,
                        TotalTokens = usage.TotalTokens
                    }
                });
            }

            var message = result.Value.Choices.FirstOrDefault()?.Message.Content.Trim();
            return ParseOpenAiResponse(message);
        }
        return result.Error;
    }

    public async Task<Result<IEnumerable<object>>> GetChatCompletionsGood(ChatCompletionRequest request, string deploymentId, string apiVersion, string? userId = null)
    {
        var result = GetChatCompletions(request, deploymentId, apiVersion);

        if (result.IsSuccess)
        {
           if (!string.IsNullOrEmpty(userId))
            {
                result.Value.Usage.LogUsage(_logger, userId);
            }

            var message = result.Value.Choices.FirstOrDefault()?.Message.Content.Trim();
            return ParseOpenAiResponse(message);
        }
        return result.Error;
    }
}