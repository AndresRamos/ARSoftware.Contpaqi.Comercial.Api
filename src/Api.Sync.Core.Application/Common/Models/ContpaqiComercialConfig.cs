using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.Common.Models;

public sealed class ContpaqiComercialConfig
{
    public string Usuario { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string RutaPlantillasPdf { get; set; } = string.Empty;
    public Empresa Empresa { get; set; } = new();
}
