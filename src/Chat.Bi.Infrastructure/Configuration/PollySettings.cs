namespace Chat.Bi.Infrastructure.Configuration;

public class PollySettings
{
    public int RetryCount { get; set; } = 3;
    public int InitialBackoffSeconds { get; set; } = 2;
    public int CircuitBreakerFailures { get; set; } = 5;
    public int CircuitBreakerDurationSeconds { get; set; } = 30;
}