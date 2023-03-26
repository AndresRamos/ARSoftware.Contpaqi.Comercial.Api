using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar un almacen.
/// </summary>
public sealed class ActualizarAlmacenRequest : ApiRequestBase, IApiRequest<ActualizarAlmacenRequestModel, ActualizarAlmacenRequestOptions>
{
    public ActualizarAlmacenRequestModel Model { get; set; } = new();
    public ActualizarAlmacenRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud ActualizarAlmacenRequest.
/// </summary>
public sealed class ActualizarAlmacenRequestModel
{
    /// <summary>
    ///     Codigo del almacen a actualizar.
    /// </summary>
    public string CodigoAlmacen { get; set; } = string.Empty;

    /// <summary>
    ///     Datos a actualizar. Los nombres deben ser igual a los de la base de datos.
    /// </summary>
    public Dictionary<string, string> DatosAlmacen { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud ActualizarAlmacenRequest.
/// </summary>
public sealed class ActualizarAlmacenRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud ActualizarAlmacenRequest.
/// </summary>
public sealed class ActualizarAlmacenResponse : ApiResponseBase, IApiResponse<ActualizarAlmacenResponseModel>
{
    public ActualizarAlmacenResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta ActualizarAlmacenResponse.
/// </summary>
public sealed class ActualizarAlmacenResponseModel
{
    public Almacen Almacen { get; set; } = new();
}
