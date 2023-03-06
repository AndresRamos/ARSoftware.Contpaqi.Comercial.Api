using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class CrearFacturaRequest : ApiRequestBase, IApiRequest<CrearFacturaRequestModel, CrearFacturaRequestOptions>
{
    public CrearFacturaRequestModel Model { get; set; } = new();
    public CrearFacturaRequestOptions Options { get; set; } = new();
}

public sealed class CrearFacturaRequestModel
{
    public Documento Documento { get; set; } = new();
}

public sealed class CrearFacturaRequestOptions
{
    public bool UsarFechaDelDia { get; set; } = true;
    public bool BuscarSiguienteFolio { get; set; } = true;
    public bool CrearCatalogos { get; set; }
    public bool Timbrar { get; set; }
    public string ContrasenaCertificado { get; set; } = string.Empty;
    public bool GenerarDocumentosDigitales { get; set; }
    public bool GenerarPdf { get; set; }
    public string NombrePlantilla { get; set; } = string.Empty;
    public bool AgregarArchivo { get; set; }
    public string NombreArchivo { get; set; } = string.Empty;
    public string ContenidoArchivo { get; set; } = string.Empty;
}

/// <inheritdoc cref="IApiResponse" />
public sealed class CrearFacturaResponse : ApiResponseBase, IApiResponse<CrearFacturaResponseModel>
{
    public CrearFacturaResponseModel Model { get; set; } = new();
}

public sealed class CrearFacturaResponseModel
{
    public Documento Documento { get; set; } = new();
    public DocumentoDigital Xml { get; set; } = new();
    public DocumentoDigital Pdf { get; set; } = new();
}
