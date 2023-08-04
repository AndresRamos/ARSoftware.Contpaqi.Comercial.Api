using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar un cliente.
/// </summary>
public sealed class
    ActualizarClienteRequest : ContpaqiRequest<ActualizarClienteRequestModel, ActualizarClienteRequestOptions, ActualizarClienteResponse>
{
    public ActualizarClienteRequest(ActualizarClienteRequestModel model, ActualizarClienteRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud ActualizarClienteRequest.
/// </summary>
public sealed class ActualizarClienteRequestModel
{
    /// <summary>
    ///     Codigo del cliente a actualizar.
    /// </summary>
    public string CodigoCliente { get; set; } = string.Empty;

    /// <summary>
    ///     Datos a actualizar. Los nombres deben ser igual a los de la base de datos.
    /// </summary>
    public Dictionary<string, string> DatosCliente { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud ActualizarClienteRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class ActualizarClienteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud ActualizarClienteRequest.
/// </summary>
public sealed class ActualizarClienteResponse : ContpaqiResponse<ActualizarClienteResponseModel>
{
    public ActualizarClienteResponse(ActualizarClienteResponseModel model) : base(model)
    {
    }

    public static ActualizarClienteResponse CreateInstance(ClienteProveedor cliente)
    {
        return new ActualizarClienteResponse(new ActualizarClienteResponseModel(cliente));
    }
}

/// <summary>
///     Modelo de la respuesta ActualizarClienteResponse.
/// </summary>
public sealed class ActualizarClienteResponseModel
{
    public ActualizarClienteResponseModel(ClienteProveedor cliente)
    {
        Cliente = cliente;
    }

    public ClienteProveedor Cliente { get; set; }
}
