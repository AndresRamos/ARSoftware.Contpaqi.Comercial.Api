using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un cliente.
/// </summary>
public sealed class CrearClienteRequest : IContpaqiRequest<CrearClienteRequestModel, CrearClienteRequestOptions>

{
    public CrearClienteRequestModel Model { get; set; } = new();
    public CrearClienteRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud CrearClienteRequest.
/// </summary>
public sealed class CrearClienteRequestModel
{
    public Cliente Cliente { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CrearClienteRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class CrearClienteRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearClienteRequest.
/// </summary>
public sealed class CrearClienteResponse : IContpaqiResponse<CrearClienteResponseModel>
{
    public CrearClienteResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta CrearClienteResponse
/// </summary>
public sealed class CrearClienteResponseModel
{
    public Cliente Cliente { get; set; } = new();
}
