namespace FileHosting.Shared.AppCore.Interfaces;

public interface IAppLogger<T>
{
    void LogInfo(string message, params object[] args);

    void LogWarning(string message, params object[] args);
}