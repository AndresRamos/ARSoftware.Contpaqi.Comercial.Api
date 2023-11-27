namespace Api.Sync.Core.Application.Common.Models;

public sealed class ApiSyncConfig
{
    private readonly DateTime _timeStarted = DateTime.Now;
    public string SubscriptionKey { get; set; } = "00000000000000000000000000000000";
    public string BaseAddress { get; set; } = string.Empty;
    public TimeOnly ShutdownTime { get; set; } = new(20, 0, 0);
    public List<string> Empresas { get; set; } = new();
    private DateTime ShutdownDateTime { get; set; }

    public bool ShouldShutDown()
    {
        return DateTime.Now >= ShutdownDateTime;
    }

    public void CalculateShutdownDateTime()
    {
        ShutdownDateTime = new DateTime(_timeStarted.Year, _timeStarted.Month, _timeStarted.Day, ShutdownTime.Hour, ShutdownTime.Minute,
            ShutdownTime.Second);

        if (_timeStarted > ShutdownDateTime) ShutdownDateTime = ShutdownDateTime.AddDays(1);
    }
}
