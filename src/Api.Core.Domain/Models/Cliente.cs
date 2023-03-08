using System.Text.Json.Serialization;
using Api.Core.Domain.Converters;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;

namespace Api.Core.Domain.Models;

/// <summary>
///     Estructura de un cliente o proveedor.
/// </summary>
public sealed class Cliente
{
    /// <summary>
    ///     Tipo de cliente.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoCliente Tipo { get; set; } = TipoCliente.ClienteProveedor;

    /// <summary>
    ///     Codigo del cliente.
    /// </summary>
    public string Codigo { get; set; } = string.Empty;

    /// <summary>
    ///     Razon social del cliente.
    /// </summary>
    public string RazonSocial { get; set; } = string.Empty;

    /// <summary>
    ///     RFC del cliente.
    /// </summary>
    public string Rfc { get; set; } = string.Empty;

    /// <summary>
    ///     Uso del CFDI del cliente.
    /// </summary>
    [JsonConverter(typeof(UsoCfdiJsonConverter))]
    public UsoCfdi? UsoCfdi { get; set; }

    /// <summary>
    ///     Regimen fiscal del cliente.
    /// </summary>
    [JsonConverter(typeof(RegimenFiscalJsonConverter))]
    public RegimenFiscal? RegimenFiscal { get; set; }

    /// <summary>
    ///     Direccion fiscal del cliente.
    /// </summary>
    public Direccion DireccionFiscal { get; set; } = new();

    /// <summary>
    ///     Datos extra del cliente.
    /// </summary>
    public Dictionary<string, string> DatosExtra { get; set; } = new();
}
