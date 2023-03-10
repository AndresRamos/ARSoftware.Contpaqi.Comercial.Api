// ReSharper disable InconsistentNaming

namespace Api.Sync.Infrastructure.ContpaqiComercial.Models;

public sealed class MovimientoSql
{
    public int CIDMOVIMIENTO { get; set; }
    public int CIDPRODUCTO { get; set; }
    public int CIDALMACEN { get; set; }
    public double CUNIDADES { get; set; }
    public double CPRECIO { get; set; }
    public double CNETO { get; set; }
    public double CTOTAL { get; set; }
    public string CREFERENCIA { get; set; } = string.Empty;
    public string COBSERVAMOV { get; set; } = string.Empty;
}
