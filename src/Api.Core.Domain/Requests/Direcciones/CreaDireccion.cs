using ARSoftware.Contpaqi.Api.Common.Domain;

namespace Api.Core.Domain.Requests.Direcciones;

/// <summary>
///     Solicitud para crear una dirección.
/// </summary>
public sealed class CreaDireccionRequest : ContpaqiRequest<CreaDireccionRequestModel, CreaDireccionRequestOptions, CreaDireccionResponse>
{
    public CreaDireccionRequest(CreaDireccionRequestModel model, CreaDireccionRequestOptions options) : base(model, options)
    {
    }
}

/// <summary>
///     Modelo de la solicitud CreaDireccionRequest.
/// </summary>
public sealed class CreaDireccionRequestModel
{
    public int CatalogoId { get; set; }
    public Direccion Direccion { get; set; } = new();
}

/// <summary>
///     Opciones de la solicitud CreaDireccionRequest.
/// </summary>
public sealed class CreaDireccionRequestOptions : ILoadRelatedDataOptions
{
    public bool CargarDatosExtra { get; set; }
}

/// <summary>
///     Respuesta de la solicitud CreaDireccionRequest.
/// </summary>
public sealed class CreaDireccionResponse : ContpaqiResponse<CreaDireccionResponseModel>
{
    public CreaDireccionResponse(CreaDireccionResponseModel model) : base(model)
    {
    }

    public static CreaDireccionResponse CreateInstance(Direccion direccion)
    {
        return new CreaDireccionResponse(new CreaDireccionResponseModel(direccion));
    }
}

/// <summary>
///     Modelo de la respuesta CreaDireccionResponse.
/// </summary>
public sealed class CreaDireccionResponseModel
{
    public CreaDireccionResponseModel(Direccion direccion)
    {
        Direccion = direccion;
    }

    public Direccion Direccion { get; }
}
