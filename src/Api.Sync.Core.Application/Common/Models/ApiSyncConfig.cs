namespace Api.Sync.Core.Application.Common.Models;

public sealed class ApiSyncConfig
{
    public string BaseAddress { get; set; } = string.Empty;
    public TimeOnly WaitTime { get; set; } = new(18, 1, 0);
    public TimeOnly ShutdownTime { get; set; } = new(18, 0, 0);
    public string EmpresaRfc { get; set; } = string.Empty;
}
