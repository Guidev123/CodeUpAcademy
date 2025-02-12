namespace CodeUp.IntegrationEvents;

public record IntegrationEventResponseMessage(bool IsValid, string[]? Errors = null)
{
    public bool IsValid { get; private set; } = IsValid;
    public string[]? Errors { get; private set; } = Errors;
}
