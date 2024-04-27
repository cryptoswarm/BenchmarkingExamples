using Microsoft.Extensions.Logging;

public class ChatCompletionResponse
 {
     public Usage Usage { get; set; }
     public Choice[] Choices { get; set; }
 }

  public class Usage
 {
     public int PromptTokens { get; set; }
     public int CompletionTokens { get; set; }
     public int TotalTokens { get; set; }

    public void LogUsage<T>(ILogger<T> logger, string userId)
    {
        logger.LogInformation("Token usage UserId : {UserId}, PromptTokens: {PromptTokens}, CompletionTokens: {CompletionTokens}, TotalTokens: {TotalTokens}", 
                                        userId,  PromptTokens, CompletionTokens, TotalTokens);
    }
 }

 public class Choice
{
    public Message Message { get; set; }
}

 public class Message
 {
     public string Content { get; set; }
 }