using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para cancelar un documento.
/// </summary>
public sealed class CancelarDocumentoRequest : 
    IContpaqiRequest<CancelarDocumentoRequestModel, CancelarDocumentoRequestOptions>
{
    public CancelarDocumentoRequestModel Model { get; set; } = new();
    public CancelarDocumentoRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud CancelarDocumentoRequest.
/// </summary>
public sealed class CancelarDocumentoRequestModel
{
    /// <summary>
    ///     Lllave del documento a cancelar.
    /// </summary>
    public LlaveDocumento LlaveDocumento { get; set; } = new();

    /// <summary>
    ///     Contrasena del certificado SAT.
    /// </summary>
    public string ContrasenaCertificado { get; set; } = string.Empty;

    /// <summary>
    ///     Motivo de la cancelacion.
    /// </summary>
    public string MotivoCancelacion { get; set; } = string.Empty;

    /// <summary>
    ///     UUID relacionado.
    /// </summary>
    public string Uuid { get; set; } = string.Empty;
}

/// <summary>
///     Opciones de la solicitud CancelarDocumentoRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class CancelarDocumentoRequestOptions : ILoadRelatedDataOptions
{
    /// <summary>
    ///     Cancelar solo administrativamente.
    /// </summary>
    public bool Administrativamente { get; set; } = false;

    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CancelarDocumentoRequest.
/// </summary>
public sealed class CancelarDocumentoResponse : IContpaqiResponse<CancelarDocumentoResponseModel>
{
    public CancelarDocumentoResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta CancelarDocumentoResponse.
/// </summary>
public sealed class CancelarDocumentoResponseModel
{
    public Documento Documento { get; set; } = new();
}
