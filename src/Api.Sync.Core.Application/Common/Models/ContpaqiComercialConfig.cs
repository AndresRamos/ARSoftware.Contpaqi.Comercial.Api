using Api.Sync.Core.Application.ContpaqiComercial.Models;

namespace Api.Sync.Core.Application.Common.Models;

public sealed class ContpaqiComercialConfig
{
    public string Usuario { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public string RutaPlantillas { get; set; } = string.Empty;
    public EmpresaDto Empresa { get; set; } = new();
}
