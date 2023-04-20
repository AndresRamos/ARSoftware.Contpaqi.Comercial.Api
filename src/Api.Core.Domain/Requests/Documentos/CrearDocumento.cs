using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para crear un documento.
/// </summary>
public sealed class CrearDocumentoRequest : IContpaqiRequest<CrearDocumentoRequestModel, CrearDocumentoRequestOptions>
{
    public CrearDocumentoRequestModel Model { get; set; } = new();
    public CrearDocumentoRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud CrearDocumentoRequest.
/// </summary>
public sealed class CrearDocumentoRequestModel
{
    public Documento Documento { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CrearDocumentoRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class CrearDocumentoRequestOptions : ILoadRelatedDataOptions
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

    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CrearDocumentoRequest.
/// </summary>
public sealed class CrearDocumentoResponse : IContpaqiResponse<CrearDocumentoResponseModel>
{
    public CrearDocumentoResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud CrearDocumentoRequest.
/// </summary>
public sealed class CrearDocumentoResponseModel
{
    public Documento Documento { get; set; } = new();
}
