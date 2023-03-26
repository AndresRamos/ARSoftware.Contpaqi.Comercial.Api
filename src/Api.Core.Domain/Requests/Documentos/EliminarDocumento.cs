using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para eliminar un documento.
/// </summary>
public sealed class EliminarDocumentoRequest : ApiRequestBase, IApiRequest<EliminarDocumentoRequestModel, EliminarDocumentoRequestOptions>
{
    public EliminarDocumentoRequestModel Model { get; set; } = new();
    public EliminarDocumentoRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud EliminarDocumentoRequest.
/// </summary>
public sealed class EliminarDocumentoRequestModel
{
    public LlaveDocumento LlaveDocumento { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud EliminarDocumentoRequest.
/// </summary>
public sealed class EliminarDocumentoRequestOptions
{
}

/// <summary>
///     Respuesta de la solicitud EliminarDocumentoRequest.
/// </summary>
public sealed class EliminarDocumentoResponse : ApiResponseBase, IApiResponse<EliminarDocumentoResponseModel>
{
    public EliminarDocumentoResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta EliminarDocumentoResponse.
/// </summary>
public sealed class EliminarDocumentoResponseModel
{
}
