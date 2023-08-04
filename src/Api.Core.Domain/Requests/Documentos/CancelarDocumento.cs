using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para cancelar un documento.
/// </summary>
public sealed class CancelarDocumentoRequest :
    ContpaqiRequest<CancelarDocumentoRequestModel, CancelarDocumentoRequestOptions, CancelarDocumentoResponse>
{
    public CancelarDocumentoRequest(CancelarDocumentoRequestModel model, CancelarDocumentoRequestOptions options) : base(model, options)
    {
    }
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
public sealed class CancelarDocumentoResponse : ContpaqiResponse<CancelarDocumentoResponseModel>
{
    public CancelarDocumentoResponse(CancelarDocumentoResponseModel model) : base(model)
    {
    }

    public static CancelarDocumentoResponse CreateInstance(Documento documento)
    {
        return new CancelarDocumentoResponse(new CancelarDocumentoResponseModel(documento));
    }
}

/// <summary>
///     Modelo de la respuesta CancelarDocumentoResponse.
/// </summary>
public sealed class CancelarDocumentoResponseModel
{
    public CancelarDocumentoResponseModel(Documento documento)
    {
        Documento = documento;
    }

    public Documento Documento { get; set; }
}
