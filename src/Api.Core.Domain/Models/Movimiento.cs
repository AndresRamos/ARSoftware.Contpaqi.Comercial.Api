namespace Api.Core.Domain.Models;

/// <summary>
///     Estructura de un movimiento.
/// </summary>
public sealed class Movimiento
{
    /// <summary>
    ///     Producto del movimiento.
    /// </summary>
    public Producto Producto { get; set; } = new();

    /// <summary>
    ///     Cantidad de unidad base del movimiento.
    /// </summary>
    public double Unidades { get; set; }

    /// <summary>
    ///     Precio del producto
    /// </summary>
    public double Precio { get; set; }

    /// <summary>
    ///     Almacen del movimiento
    /// </summary>
    public Almacen Almacen { get; set; } = new();

    /// <summary>
    ///     Referencia del movimiento.
    /// </summary>
    public string Referencia { get; set; } = string.Empty;

    /// <summary>
    ///     Observaciones del movimiento.
    /// </summary>
    public string Observaciones { get; set; } = string.Empty;

    /// <summary>
    ///     Datos extra del movimiento.
    /// </summary>
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
