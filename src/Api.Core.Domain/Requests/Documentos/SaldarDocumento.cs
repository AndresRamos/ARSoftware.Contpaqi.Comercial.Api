using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para saldar un documento.
/// </summary>
public sealed class SaldarDocumentoRequest : ApiRequestBase, IApiRequest<SaldarDocumentoRequestModel, SaldarDocumentoRequestOptions>
{
    public SaldarDocumentoRequestModel Model { get; set; } = new();
    public SaldarDocumentoRequestOptions Options { get; set; } = new();
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
public sealed class SaldarDocumentoResponse : ApiResponseBase, IApiResponse<SaldarDocumentoResponseModel>
{
    public SaldarDocumentoResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta SaldarDocumentoResponse.
/// </summary>
public sealed class SaldarDocumentoResponseModel
{
    public Documento DocumentoPago { get; set; } = new();
    public Documento DocumentoPagar { get; set; } = new();
}
