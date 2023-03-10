// ReSharper disable InconsistentNaming

namespace Api.Sync.Infrastructure.ContpaqiComercial.Models;

public sealed class AgenteSql
{
    public int CIDAGENTE { get; set; }
    public string CCODIGOAGENTE { get; set; } = string.Empty;
    public string CNOMBREAGENTE { get; set; } = string.Empty;
    public int CTIPOAGENTE { get; set; }
}
