namespace Api.Core.Domain.Models;

/// <summary>
///     Estructura de un almacen.
/// </summary>
public sealed class Almacen
{
    /// <summary>
    ///     Codigo del almacen.
    /// </summary>
    public string Codigo { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del almacen.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    ///     Datos extra del almacen.
    /// </summary>
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
