using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <summary>
///     Solicitud para buscar clientes.
/// </summary>
public sealed class BuscarClientesRequest : ApiRequestBase, IApiRequest<BuscarClientesRequestModel, BuscarClientesRequestOptions>
{
    public BuscarClientesRequestModel Model { get; set; } = new();
    public BuscarClientesRequestOptions Options { get; set; } = new();
}

/// <summary>
///     Modelo de la solicitud BuscarClientesRequest.
/// </summary>
public sealed class BuscarClientesRequestModel
{
    /// <summary>
    ///     Parametro para buscar clientes por id.
    /// </summary>
    public int? Id { get; set; }

    /// <summary>
    ///     Parametro para buscar clientes por codigo.
    /// </summary>
    public string? Codigo { get; set; }

    /// <summary>
    ///     Parametro para buscar clientes por SQL. El valor debe ser el WHERE clause y debes asegurarte de sanatizar tu SQL.
    ///     <example>
    ///         <para>Ejemplo para buscar clientes por razon social:</para>
    ///         <code>SqlQuery = "CRAZONSOCIAL = 'razonSocial'"</code>
    ///         <para>se traduce a </para>
    ///         <code>SELECT * FROM admClientes WHERE CRAZONSOCIAL = 'razonSocial'</code>
    ///     </example>
    /// </summary>
    public string? SqlQuery { get; set; }
}

/// <summary>
///     Opciones de la solicitud BuscarClientesRequest.
/// </summary>
/// <inheritdoc cref="ILoadRelatedDataOptions" />
public sealed class BuscarClientesRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud BuscarClientesRequest.
/// </summary>
public sealed class BuscarClientesResponse : ApiResponseBase, IApiResponse<BuscarClientesResponseModel>
{
    public BuscarClientesResponseModel Model { get; set; } = new();
}

/// <summary>
///     Modelo de la respuesta BuscarClientesResponse.
/// </summary>
public sealed class BuscarClientesResponseModel
{
    /// <summary>
    ///     Lista de clientes encontrados.
    /// </summary>
    public List<Cliente> Clientes { get; set; } = new();
}
