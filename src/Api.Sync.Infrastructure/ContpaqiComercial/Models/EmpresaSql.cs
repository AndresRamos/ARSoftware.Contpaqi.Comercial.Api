// ReSharper disable InconsistentNaming

namespace Api.Sync.Infrastructure.ContpaqiComercial.Models;

public sealed class EmpresaSql
{
    public int CIDEMPRESA { get; set; }
    public string CNOMBREEMPRESA { get; set; } = string.Empty;
    public string CRUTADATOS { get; set; } = string.Empty;
}

public sealed class ParametrosSql
{
    public string CGUIDDSL { get; set; } = string.Empty;
    public string CRFCEMPRESA { get; set; } = string.Empty;
}
