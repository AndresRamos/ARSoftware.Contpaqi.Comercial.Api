using Api.Core.Domain.Models;

namespace Api.Core.Domain.Requests;

/// <inheritdoc cref="IApiRequest" />
public sealed class CrearAlmacenRequest : ApiRequestBase, IApiRequest<CrearAlmacenRequestModel, CrearAlmacenRequestOptions>
{
    public CrearAlmacenRequestModel Model { get; set; } = new();
    public CrearAlmacenRequestOptions Options { get; set; } = new();
}

public sealed class CrearAlmacenRequestModel
{
    public Almacen Almacen { get; set; } = new();
}

public sealed class CrearAlmacenRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <inheritdoc cref="IApiResponse" />
public sealed class CrearAlmacenResponse : ApiResponseBase, IApiResponse<CrearAlmacenResponseModel>
{
    public CrearAlmacenResponseModel Model { get; set; } = new();
}

public sealed class CrearAlmacenResponseModel
{
    public Almacen Almacen { get; set; } = new();
}
