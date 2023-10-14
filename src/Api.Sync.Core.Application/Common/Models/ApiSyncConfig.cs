namespace Api.Sync.Core.Application.Common.Models;

public sealed class ApiSyncConfig
{
    private readonly DateTime _timeStarted = DateTime.Now;

    public ApiSyncConfig()
    {
        ShutdownDateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, ShutdownTime.Hour, ShutdownTime.Minute,
            ShutdownTime.Second);

        if (_timeStarted > ShutdownDateTime) ShutdownDateTime = ShutdownDateTime.AddDays(1);
    }

    public string SubscriptionKey { get; set; } = "00000000000000000000000000000000";
    public string BaseAddress { get; set; } = string.Empty;
    public TimeOnly ShutdownTime { get; set; } = new(20, 0, 0);
    public List<string> Empresas { get; set; } = new();
    private DateTime ShutdownDateTime { get; }

    public bool ShouldShutDown()
    {
        return DateTime.Now >= ShutdownDateTime;
    }
}