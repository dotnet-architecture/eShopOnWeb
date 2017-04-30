namespace ApplicationCore.Interfaces
{
    public interface IAppLogger<T>
    {
        void LogWarning(string message, params object[] args);
    }
}
