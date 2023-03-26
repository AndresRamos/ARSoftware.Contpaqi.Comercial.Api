using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar un documento.
/// </summary>
public sealed class ActualizarDocumentoRequest : ApiRequestBase,
    IApiRequest<ActualizarDocumentoRequestModel, ActualizarDocumentoRequestOptions>
{
    public ActualizarDocumentoRequestModel Model { get; set; } = new();
    public ActualizarDocumentoRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud ActualizarDocumentoRequest.
/// </summary>
public sealed class ActualizarDocumentoRequestModel
{
    /// <summary>
    ///     Llave del documento a actualizar.
    /// </summary>
    public LlaveDocumento LlaveDocumento { get; set; } = new();

    /// <summary>
    ///     Datos a actualizar. Los nombres deben ser igual a los de la base de datos.
    /// </summary>
    public Dictionary<string, string> DatosDocumento { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud ActualizarDocumentoRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class ActualizarDocumentoRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud ActualizarDocumentoRequest.
/// </summary>
public sealed class ActualizarDocumentoResponse : ApiResponseBase, IApiResponse<ActualizarDocumentoResponseModel>
{
    public ActualizarDocumentoResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta ActualizarDocumentoResponse.
/// </summary>
public sealed class ActualizarDocumentoResponseModel
{
    public Documento Documento { get; set; } = new();
}
