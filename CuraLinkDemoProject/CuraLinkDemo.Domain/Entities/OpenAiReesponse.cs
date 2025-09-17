﻿namespace CuraLinkDemoProject.CuraLinkDemo.Domain.Entities
{
    public class OpenAiResponse
    {
        public string Id { get; set; } = string.Empty;
        public string Object { get; set; } = string.Empty;
        public int Created { get; set; }
        public string Model { get; set; } = string.Empty;
        public List<Choice> Choices { get; set; } = new();
        public Usage Usage { get; set; } = new();
    }

    public class Choice
    {
        public int Index { get; set; }
        public Message Message { get; set; } = new();
        public string FinishReason { get; set; } = string.Empty;
    }

    public class Message
    {
        public string Role { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }

    public class Usage
    {
        public int PromptTokens { get; set; }
        public int CompletionTokens { get; set; }
        public int TotalTokens { get; set; }
    }
}
