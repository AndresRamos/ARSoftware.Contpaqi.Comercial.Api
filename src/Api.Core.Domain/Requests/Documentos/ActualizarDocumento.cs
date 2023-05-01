using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para actualizar un documento.
/// </summary>
public sealed class ActualizarDocumentoRequest : ContpaqiRequest<ActualizarDocumentoRequestModel, ActualizarDocumentoRequestOptions,
    ActualizarDocumentoResponse>
{
    public ActualizarDocumentoRequest(ActualizarDocumentoRequestModel model, ActualizarDocumentoRequestOptions options) : base(model,
        options)
    {
    }
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
public sealed class ActualizarDocumentoResponse : ContpaqiResponse<ActualizarDocumentoResponseModel>
{
    public ActualizarDocumentoResponse(ActualizarDocumentoResponseModel model) : base(model)
    {
    }

    public static ActualizarDocumentoResponse CreateInstance(Documento documento)
    {
        return new ActualizarDocumentoResponse(new ActualizarDocumentoResponseModel(documento));
    }
}

/// <summary>
///     Modelo de la respuesta ActualizarDocumentoResponse.
/// </summary>
public sealed class ActualizarDocumentoResponseModel
{
    public ActualizarDocumentoResponseModel(Documento documento)
    {
        Documento = documento;
    }

    public Documento Documento { get; set; }
}
