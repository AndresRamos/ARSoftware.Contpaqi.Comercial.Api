namespace Api.Sync.Core.Application.Common.Models;

public sealed class ApiSyncConfig
{
    private readonly TimeOnly _timeStarted = TimeOnly.FromDateTime(DateTime.Now);
    public string BaseAddress { get; set; } = string.Empty;
    public TimeOnly WaitTime { get; set; } = TimeOnly.MinValue;
    public TimeOnly ShutdownTime { get; set; } = new(18, 0, 0);
    public string EmpresaRfc { get; set; } = string.Empty;

    public bool ShouldShutDown()
    {
        bool startedAfterShutdownTime = _timeStarted > ShutdownTime;

        bool isPastShutdownTime = TimeOnly.FromDateTime(DateTime.Now) >= ShutdownTime;

        return !startedAfterShutdownTime && isPastShutdownTime;
    }
}
