using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class ActualizarAlmacenRequest : ApiRequestBase, IApiRequest<ActualizarAlmacenRequestModel, ActualizarAlmacenRequestOptions>
{
    public ActualizarAlmacenRequestModel Model { get; set; } = new();
    public ActualizarAlmacenRequestOptions Options { get; set; } = new();
}

public sealed class ActualizarAlmacenRequestModel
{
    public string CodigoAlmacen { get; set; } = string.Empty;
    public Dictionary<string, string> DatosAlmacen { get; set; } = new();
}

public sealed class ActualizarAlmacenRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class ActualizarAlmacenResponse : ApiResponseBase, IApiResponse<ActualizarAlmacenResponseModel>
{
    public ActualizarAlmacenResponseModel Model { get; set; } = new();
}

public sealed class ActualizarAlmacenResponseModel
{
    public Almacen Almacen { get; set; } = new();
}
