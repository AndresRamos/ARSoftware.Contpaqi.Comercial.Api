using System.Text.Json.Serialization;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;

namespace Api.Core.Domain.Models;

/// <summary>
///     Estructura de un agente.
/// </summary>
public sealed class Agente
{
    /// <summary>
    ///     Codigo del agente.
    /// </summary>
    public string Codigo { get; set; } = string.Empty;

    /// <summary>
    ///     Nombre del agente.
    /// </summary>
    public string Nombre { get; set; } = string.Empty;

    /// <summary>
    ///     Tipo de agente: 1 = Agente de Ventas. 2 = Agente Venta / Cobro. 3 = Agente de Cobro.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoAgente Tipo { get; set; } = TipoAgente.VentasCobro;

    /// <summary>
    ///     Datos extra del agente.
    /// </summary>
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
