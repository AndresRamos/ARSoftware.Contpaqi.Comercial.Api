namespace Api.Sync.Core.Application.Common.Models;

public sealed class ContpaqiComercialConfig
{
    public string Usuario { get; set; } = string.Empty;
    public string Contrasena { get; set; } = string.Empty;
    public bool HayIntefazConEmpresaContabilidad { get; set; }
    public string RutaPlantillasPdf { get; set; } = string.Empty;
    public Empresa Empresa { get; set; } = new();
    public Dictionary<string, string> EmpresasMap { get; set; } = new();
}
