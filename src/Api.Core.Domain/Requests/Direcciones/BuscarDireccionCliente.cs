using System.Text.Json.Serialization;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar una dirección de un cliente.
/// </summary>
public sealed class BuscarDireccionClienteRequest : ContpaqiRequest<BuscarDireccionClienteRequestModel, BuscarDireccionClienteRequestOptions
    , BuscarDireccionClienteResponse>
{
    public BuscarDireccionClienteRequest(BuscarDireccionClienteRequestModel model, BuscarDireccionClienteRequestOptions options) :
        base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud BuscarDireccionClienteRequest.
/// </summary>
public sealed class BuscarDireccionClienteRequestModel
{
    /// <summary>
    ///     Código del cliente.
    /// </summary>
    public string CodigoCliente { get; set; }

    /// <summary>
    ///     Tipo de direccion.
    /// </summary>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TipoDireccion Tipo { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarDireccionClienteRequest.
/// </summary>
public sealed class BuscarDireccionClienteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarDireccionClienteRequest.
/// </summary>
public sealed class BuscarDireccionClienteResponse : ContpaqiResponse<BuscarDireccionClienteResponseModel>
{
    public BuscarDireccionClienteResponse(BuscarDireccionClienteResponseModel model) : base(model)
    {
    }

    public static BuscarDireccionClienteResponse CreateInstance(Direccion? direccion)
    {
        return new BuscarDireccionClienteResponse(new BuscarDireccionClienteResponseModel(direccion));
    }
}

/// <summary>
///     Modelo de la respuesta BuscarDireccionClienteResponse.
/// </summary>
public sealed class BuscarDireccionClienteResponseModel
{
    public BuscarDireccionClienteResponseModel(Direccion? direccion)
    {
        Direccion = direccion;
    }

    public Direccion? Direccion { get; }
}
