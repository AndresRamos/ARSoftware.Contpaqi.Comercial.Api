namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para eliminar un cliente.
/// </summary>
public sealed class EliminarClienteRequest : ApiRequestBase, IApiRequest<EliminarClienteRequestModel, EliminarClienteRequestOptions>
{
    public EliminarClienteRequestModel Model { get; set; } = new();
    public EliminarClienteRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud EliminarClienteRequest.
/// </summary>
public sealed class EliminarClienteRequestModel
{
    /// <summary>
    ///     Codigo del cliente a eliminar.
    /// </summary>
    public string CodigoCliente { get; set; } = string.Empty;
}

/// <summary>
///     Opciones de la solicitud EliminarClienteRequest.
/// </summary>
public sealed class EliminarClienteRequestOptions
{
}

/// <summary>
///     Respuesta de la solicitud EliminarClienteRequest.
/// </summary>
public sealed class EliminarClienteResponse : ApiResponseBase, IApiResponse<EliminarClienteResponseModel>
{
    public EliminarClienteResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta EliminarClienteResponse.
/// </summary>
public sealed class EliminarClienteResponseModel
{
}
