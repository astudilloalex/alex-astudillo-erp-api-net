namespace AlexAstudilloERP.Domain.Interfaces.Services.Custom;

/// <summary>
/// Manage the app logs.
/// </summary>
public interface ILoggerManager
{
    /// <summary>
    /// Log when debug.
    /// </summary>
    /// <param name="message">The message or info to log.</param>
    public void LogDebug(string message);

    /// <summary>
    /// Log when occurs a error.
    /// </summary>
    /// <param name="message">The message or info to log.</param>
    public void LogError(string message);

    /// <summary>
    /// Information log..
    /// </summary>
    /// <param name="message">The message or info to log.</param>
    public void LogInfo(string message);

    /// <summary>
    /// Warning log.
    /// </summary>
    /// <param name="message">The message or info to log.</param>
    public void LogWarning(string message);
}
