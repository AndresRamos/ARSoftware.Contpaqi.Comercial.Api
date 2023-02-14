namespace Api.Core.Domain.Models;

/// <summary>
///     Estructura de un producto.
/// </summary>
public sealed class Producto
{
    /// <summary>
    ///     Codigo del producto.
    /// </summary>
    public string Codigo { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del producto.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    ///     Datos extra del producto.
    /// </summary>
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
