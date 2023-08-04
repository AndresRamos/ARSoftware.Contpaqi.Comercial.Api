using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class ProductoFactory
{
    public const string Codigo = "PROD001";
    public const string Nombre = "Producto 1";
    public const string ClaveSat = "43231500";

    private static CrearProductoRequest Crear()
    {
        var request = new CrearProductoRequest(new CrearProductoRequestModel(), new CrearProductoRequestOptions());

        request.Model.Producto = CrearProductoPrueba();

        return request;
    }

    private static ActualizarProductoRequest Actualizar()
    {
        var request = new ActualizarProductoRequest(new ActualizarProductoRequestModel(), new ActualizarProductoRequestOptions());

        request.Model.CodigoProducto = Codigo;
        request.Model.DatosProducto = GetDatosExtra();

        return request;
    }

    private static BuscarProductosRequest BuscarPorId()
    {
        var request = new BuscarProductosRequest(new BuscarProductosRequestModel(), new BuscarProductosRequestOptions());

        request.Model.Id = 100;

        return request;
    }

    private static BuscarProductosRequest BuscarPorCodigo()
    {
        var request = new BuscarProductosRequest(new BuscarProductosRequestModel(), new BuscarProductosRequestOptions());

        request.Model.Codigo = Codigo;

        return request;
    }

    private static BuscarProductosRequest BuscarPorSql()
    {
        var request = new BuscarProductosRequest(new BuscarProductosRequestModel(), new BuscarProductosRequestOptions());

        request.Model.SqlQuery = "CNOMBREPRODUCTO = 'nombre'";

        return request;
    }

    private static BuscarProductosRequest BuscarTodo()
    {
        var request = new BuscarProductosRequest(new BuscarProductosRequestModel(), new BuscarProductosRequestOptions());

        return request;
    }

    private static EliminarProductoRequest Eliminar()
    {
        var request = new EliminarProductoRequest(new EliminarProductoRequestModel(), new EliminarProductoRequestOptions());

        request.Model.CodigoProducto = Codigo;

        return request;
    }

    private static Dictionary<string, string> GetDatosExtra()
    {
        return new Dictionary<string, string>
        {
            { nameof(admProductos.CFECHAALTAPRODUCTO), DateTime.Today.ToSdkFecha() },
            { nameof(admProductos.CNOMBREPRODUCTO), Nombre },
            { nameof(admProductos.CCLAVESAT), ClaveSat },
            { nameof(admProductos.CTEXTOEXTRA1), "Texto Extra" }
        };
    }

    public static Producto CrearProductoPrueba()
    {
        var producto = new Producto { Codigo = Codigo, Nombre = Nombre, ClaveSat = ClaveSat, DatosExtra = GetDatosExtra() };
        return producto;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearProductoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarProductoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Actualizar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarProductosRequest)}_PorId.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarProductosRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarProductosRequest)}_PorSql.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorSql(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarProductosRequest)}_Todo.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarTodo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarProductoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Eliminar(), options));
    }
}
