namespace Chat.Bi.Infrastructure.Logging;

public interface IAppLogger<T>
{
    void LogInformation(string message, params object[] args);
    void LogWarning(string message, params object[] args);
    void LogError(object exception, string message, params object[] args);
    void LogDebug(string message, params object[] args);
}