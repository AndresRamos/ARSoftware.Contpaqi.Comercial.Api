using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class ProductoFactory
{
    public const string CodigoPrueba = "PROD001";

    private static CrearProductoRequest GetCrearProductoRequest()
    {
        return new CrearProductoRequest(new CrearProductoRequestModel { Producto = GetModeloPrueba() }, new CrearProductoRequestOptions());
    }

    private static CrearProductoResponse GetCrearProductoResponse()
    {
        return new CrearProductoResponse(new CrearProductoResponseModel(GetModeloPrueba()));
    }

    private static ActualizarProductoRequest GetActualizarProductoRequest()
    {
        return new ActualizarProductoRequest(
            new ActualizarProductoRequestModel { CodigoProducto = CodigoPrueba, DatosProducto = GetDatosExtraPrueba() },
            new ActualizarProductoRequestOptions());
    }

    private static ActualizarProductoResponse GetActualizarProductoResponse()
    {
        return new ActualizarProductoResponse(new ActualizarProductoResponseModel(GetModeloPrueba()));
    }

    private static BuscarProductosRequest GetBuscarProductosRequest()
    {
        return new BuscarProductosRequest(
            new BuscarProductosRequestModel
            {
                Id = 1, Codigo = CodigoPrueba, SqlQuery = $"{nameof(admProductos.CNOMBREPRODUCTO)} = 'PRODUCTO DE PRUEBAS"
            }, new BuscarProductosRequestOptions());
    }

    private static BuscarProductosResponse GetBuscarProductosResponse()
    {
        return new BuscarProductosResponse(new BuscarProductosResponseModel(new List<Producto> { GetModeloPrueba() }));
    }

    private static EliminarProductoRequest GetEliminarProductoRequest()
    {
        return new EliminarProductoRequest(new EliminarProductoRequestModel { CodigoProducto = CodigoPrueba },
            new EliminarProductoRequestOptions());
    }

    private static EliminarProductoResponse GetEliminarProductoResponse()
    {
        return new EliminarProductoResponse(new EliminarProductoResponseModel());
    }

    private static Producto GetModeloPrueba()
    {
        return new Producto
        {
            Codigo = CodigoPrueba, Nombre = "PRODUCTO DE PRUEBAS", ClaveSat = "43231500", DatosExtra = GetDatosExtraPrueba()
        };
    }

    private static Dictionary<string, string> GetDatosExtraPrueba()
    {
        return new Dictionary<string, string>
        {
            { nameof(admProductos.CCLAVESAT), "43231500" },
            { nameof(admProductos.CTEXTOEXTRA1), "Texto extra 1" },
            { nameof(admProductos.CTEXTOEXTRA2), "Texto extra 2" },
            { nameof(admProductos.CTEXTOEXTRA3), "Texto extra 3" }
        };
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(Producto)}.json"), JsonSerializer.Serialize(GetModeloPrueba(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearProductoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetCrearProductoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearProductoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetCrearProductoResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarProductoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetActualizarProductoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarProductoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetActualizarProductoResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarProductosRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarProductosRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarProductosResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarProductosResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarProductoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetEliminarProductoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarProductoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetEliminarProductoResponse(), options));
    }
}
