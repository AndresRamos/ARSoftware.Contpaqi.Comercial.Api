using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un almacen.
/// </summary>
public sealed class CrearAlmacenRequest : IContpaqiRequest<CrearAlmacenRequestModel, CrearAlmacenRequestOptions>
{
    public CrearAlmacenRequestModel Model { get; set; } = new();
    public CrearAlmacenRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud CrearAlmacenRequest.
/// </summary>
public sealed class CrearAlmacenRequestModel
{
    public Almacen Almacen { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CrearAlmacenRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class CrearAlmacenRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearAlmacenRequest.
/// </summary>
public sealed class CrearAlmacenResponse : IContpaqiResponse<CrearAlmacenResponseModel>
{
    public CrearAlmacenResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta CrearAlmacenResponse.
/// </summary>
public sealed class CrearAlmacenResponseModel
{
    public Almacen Almacen { get; set; } = new();
}
