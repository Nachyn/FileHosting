using FileHosting.Shared.AppCore.Interfaces;
using Microsoft.Extensions.Logging;

namespace FileHosting.Shared.Infrastructure.Logging;

public class LoggerAdapter<T> : IAppLogger<T>
{
    private readonly ILogger<T> _logger;

    public LoggerAdapter(ILogger<T> logger)
    {
        _logger = logger;
    }

    public void LogInfo(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }
}