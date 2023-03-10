// ReSharper disable InconsistentNaming

namespace Api.Sync.Infrastructure.ContpaqiComercial.Models;

public sealed class ProductoSql
{
    public int CIDPRODUCTO { get; set; }
    public string CCODIGOPRODUCTO { get; set; } = string.Empty;
    public string CNOMBREPRODUCTO { get; set; } = string.Empty;
    public int CTIPOPRODUCTO { get; set; }
    public string CCLAVESAT { get; set; } = string.Empty;
}
