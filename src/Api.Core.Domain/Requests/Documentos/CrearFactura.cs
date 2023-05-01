using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear una factura.
/// </summary>
public sealed class CrearFacturaRequest : ContpaqiRequest<CrearFacturaRequestModel, CrearFacturaRequestOptions, CrearFacturaResponse>
{
    public CrearFacturaRequest(CrearFacturaRequestModel model, CrearFacturaRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud CrearFacturaRequest.
/// </summary>
public sealed class CrearFacturaRequestModel
{
    public Documento Documento { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CrearFacturaRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class CrearFacturaRequestOptions : ILoadRelatedDataOptions
{
    /// <summary>
    ///     Ignora la fecha del documento y usa la fecha del dia en que se crea el documento.
    /// </summary>
    public bool UsarFechaDelDia { get; set; } = true;

    /// <summary>
    ///     Ignora la serie y folio del documento y usa el folio consecutivo del concepto.
    /// </summary>
    public bool BuscarSiguienteFolio { get; set; } = true;

    /// <summary>
    ///     Crea los catalogos si no existen.
    /// </summary>
    public bool CrearCatalogos { get; set; }

    public bool Timbrar { get; set; }
    public string ContrasenaCertificado { get; set; } = string.Empty;
    public bool GenerarDocumentosDigitales { get; set; }
    public bool GenerarPdf { get; set; }
    public string NombrePlantilla { get; set; } = string.Empty;
    public bool AgregarArchivo { get; set; }
    public string NombreArchivo { get; set; } = string.Empty;
    public string ContenidoArchivo { get; set; } = string.Empty;
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearFacturaRequest.
/// </summary>
public sealed class CrearFacturaResponse : ContpaqiResponse<CrearFacturaResponseModel>
{
    public CrearFacturaResponse(CrearFacturaResponseModel model) : base(model)
    {
    }

    public static CrearFacturaResponse CreateInstance(Documento documento, DocumentoDigital xml, DocumentoDigital pdf)
    {
        return new CrearFacturaResponse(new CrearFacturaResponseModel(documento, xml, pdf));
    }
}

/// <summary>
///     Modelo de la respuesta CrearFacturaRequest.
/// </summary>
public sealed class CrearFacturaResponseModel
{
    public CrearFacturaResponseModel(Documento documento, DocumentoDigital xml, DocumentoDigital pdf)
    {
        Documento = documento;
        Xml = xml;
        Pdf = pdf;
    }

    public Documento Documento { get; set; }
    public DocumentoDigital Xml { get; set; }
    public DocumentoDigital Pdf { get; set; }
}
