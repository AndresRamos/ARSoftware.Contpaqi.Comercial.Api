using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para timbrar un documento.
/// </summary>
public sealed class TimbrarDocumentoRequest : ApiRequestBase, IApiRequest<TimbrarDocumentoRequestModel, TimbrarDocumentoRequestOptions>
{
    public TimbrarDocumentoRequestModel Model { get; set; } = new();
    public TimbrarDocumentoRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud TimbrarDocumentoRequest.
/// </summary>
public sealed class TimbrarDocumentoRequestModel
{
    public LlaveDocumento LlaveDocumento { get; set; } = new();

    /// <summary>
    ///     Contrasena del certificado SAT.
    /// </summary>
    public string ContrasenaCertificado { get; set; } = string.Empty;
}

/// <summary>
///     Opciones de la solicitud TimbrarDocumentoRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class TimbrarDocumentoRequestOptions : ILoadRelatedDataOptions
{
    /// <summary>
    ///     Agregar archivo de complemento adicional.
    /// </summary>
    public bool AgregarArchivo { get; set; }

    /// <summary>
    ///     Nombre del archivo adicional.
    /// </summary>
    public string NombreArchivo { get; set; } = string.Empty;

    /// <summary>
    ///     Contenido del archivo adicional.
    /// </summary>
    public string ContenidoArchivo { get; set; } = string.Empty;

    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud TimbrarDocumentoRequest.
/// </summary>
public sealed class TimbrarDocumentoResponse : ApiResponseBase, IApiResponse<TimbrarDocumentoResponseModel>
{
    public TimbrarDocumentoResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta TimbrarDocumentoResponse.
/// </summary>
public sealed class TimbrarDocumentoResponseModel
{
    public Documento Documento { get; set; } = new();
}
