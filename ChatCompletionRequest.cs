public record ChatCompletionRequest
{
    public float Temperature { get; init; }
    public int MaxTokens { get; init; } = 200;
    public float TopP { get; init; }
    public float PresencePenalty { get; init; }
    public float FrequencyPenalty { get; init; }
    public IList<Message> Messages { get; init; } = new List<Message>();
}