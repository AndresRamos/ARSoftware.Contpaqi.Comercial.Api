namespace Api.Core.Domain.Models;

public class SeriesCapas
{
    /// <summary>
    ///     Unidades del movimiento.
    /// </summary>
    public double Unidades { get; set; }

    /// <summary>
    ///     Tipo de cambio del movimiento.
    /// </summary>
    public decimal TipoCambio { get; set; }

    /// <summary>
    ///     Series del movimiento.
    /// </summary>
    public string Series { get; set; } = string.Empty;

    /// <summary>
    ///     Pedimento del movimiento.
    /// </summary>
    public string Pedimento { get; set; } = string.Empty;

    /// <summary>
    ///     Agencia aduanal del movimiento.
    /// </summary>
    public string Agencia { get; set; } = string.Empty;

    /// <summary>
    ///     Fecha de pedimento del movimiento.
    /// </summary>
    public DateTime FechaPedimento { get; set; } = DateTime.MinValue;

    /// <summary>
    ///     Número de lote del movimiento.
    /// </summary>
    public string NumeroLote { get; set; } = string.Empty;

    /// <summary>
    ///     Fecha de fabricación del movimiento.
    /// </summary>
    public DateTime FechaFabricacion { get; set; } = DateTime.MinValue;

    /// <summary>
    ///     Fecha de Caducidad del movimiento.
    /// </summary>
    public DateTime FechaCaducidad { get; set; } = DateTime.MinValue;
}
