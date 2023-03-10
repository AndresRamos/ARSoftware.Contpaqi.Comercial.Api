// ReSharper disable InconsistentNaming

namespace Api.Sync.Infrastructure.ContpaqiComercial.Models;

public sealed class ClienteSql
{
    public int CIDCLIENTEPROVEEDOR { get; set; }
    public string CCODIGOCLIENTE { get; set; } = string.Empty;
    public string CRAZONSOCIAL { get; set; } = string.Empty;
    public int CTIPOCLIENTE { get; set; }
    public string CRFC { get; set; } = string.Empty;
    public string CUSOCFDI { get; set; } = string.Empty;
    public string CREGIMFISC { get; set; } = string.Empty;
}
