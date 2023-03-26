using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar documentos.
/// </summary>
public sealed class BuscarDocumentosRequest : ApiRequestBase, IApiRequest<BuscarDocumentosRequestModel, BuscarDocumentosRequestOptions>
{
    public BuscarDocumentosRequestModel Model { get; set; } = new();

    public BuscarDocumentosRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud BuscarDocumentosRequest.
/// </summary>
public sealed class BuscarDocumentosRequestModel
{
    /// <summary>
    ///     Parametro para buscar documentos por id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    ///     Parametro para buscar documentos por llave.
    /// </summary>
    public LlaveDocumento? Llave { get; set; }

    /// <summary>
    ///     Parametro para buscar documentos por concepto.
    /// </summary>
    public string? ConceptoCodigo { get; set; }

    /// <summary>
    ///     Parametro para buscar documentos por rango de fecha.
    /// </summary>
    public DateOnly? FechaInicio { get; set; }

    /// <summary>
    ///     Parametro para buscar documentos por rango de fecha.
    /// </summary>
    public DateOnly? FechaFin { get; set; }

    /// <summary>
    ///     Parametro para buscar documentos por cliente.
    /// </summary>
    public string? ClienteCodigo { get; set; }

    /// <summary>
    ///     Parametro para buscar documentos por SQL. El valor debe ser el WHERE clause y debes asegurarte de sanatizar tu SQL.
    ///     <example>
    ///         <para>Ejemplo para buscar documentos con saldo pendiente:</para>
    ///         <code>SqlQuery = "CPENDIENTE > 0.00"</code>
    ///         <para>se traduce a </para>
    ///         <code>SELECT * FROM admDocumentos WHERE CPENDIENTE > 0.00</code>
    ///     </example>
    /// </summary>
    public string? SqlQuery { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarDocumentosRequest.
/// </summary>
public sealed class BuscarDocumentosRequestOptions : ILoadRelatedDataOptions
{
    /// <summary>
    ///     Opcion para cargar los datos extra de los objectos.
    /// </summary>
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarDocumentosRequest.
/// </summary>
public sealed class BuscarDocumentosResponse : ApiResponseBase, IApiResponse<BuscarDocumentosResponseModel>
{
    public BuscarDocumentosResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta BuscarDocumentosResponse.
/// </summary>
public sealed class BuscarDocumentosResponseModel
{
    /// <summary>
    ///     Lista de documentos encontrados.
    /// </summary>
    public List<Documento> Documentos { get; set; } = new();
}
