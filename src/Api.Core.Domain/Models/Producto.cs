using System.Text.Json.Serialization;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;

namespace Api.Core.Domain.Models;

/// <summary>
///     Estructura de un producto.
/// </summary>
public sealed class Producto
{
    /// <summary>
    ///     Tipo de producto.Tipo de producto: 1 = Producto 2 = Paquete 3 = Servicio
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoProducto Tipo { get; set; } = TipoProducto.Producto;

    /// <summary>
    ///     Codigo del producto.
    /// </summary>
    public string Codigo { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del producto.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    ///     Clave SAT del producto.
    /// </summary>
    public string ClaveSat { get; set; } = string.Empty;

    /// <summary>
    ///     Datos extra del producto.
    /// </summary>
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
