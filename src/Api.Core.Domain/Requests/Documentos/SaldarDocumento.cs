using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para saldar un documento.
/// </summary>
public sealed class
    SaldarDocumentoRequest : ContpaqiRequest<SaldarDocumentoRequestModel, SaldarDocumentoRequestOptions, SaldarDocumentoResponse>
{
    public SaldarDocumentoRequest(SaldarDocumentoRequestModel model, SaldarDocumentoRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud SaldarDocumentoRequest.
/// </summary>
public sealed class SaldarDocumentoRequestModel
{
    public LlaveDocumento DocumentoAPagar { get; set; } = new();
    public LlaveDocumento DocumentoPago { get; set; } = new();

    /// <summary>
    ///     Fecha del documento a crear.
    /// </summary>
    public DateTime Fecha { get; set; } = DateTime.Today;

    /// <summary>
    ///     Importe a abonar.
    /// </summary>
    public decimal Importe { get; set; }
}

/// <summary>
///     Opciones de la solicitud SaldarDocumentoRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class SaldarDocumentoRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud SaldarDocumentoRequest.
/// </summary>
public sealed class SaldarDocumentoResponse : ContpaqiResponse<SaldarDocumentoResponseModel>
{
    public SaldarDocumentoResponse(SaldarDocumentoResponseModel model) : base(model)
    {
    }

    public static SaldarDocumentoResponse CreateInstance(Documento documentoPago, Documento documentoPagar)
    {
        return new SaldarDocumentoResponse(new SaldarDocumentoResponseModel(documentoPago, documentoPagar));
    }
}

/// <summary>
///     Modelo de la respuesta SaldarDocumentoResponse.
/// </summary>
public sealed class SaldarDocumentoResponseModel
{
    public SaldarDocumentoResponseModel(Documento documentoPago, Documento documentoPagar)
    {
        DocumentoPago = documentoPago;
        DocumentoPagar = documentoPagar;
    }

    public Documento DocumentoPago { get; set; }
    public Documento DocumentoPagar { get; set; }
}
