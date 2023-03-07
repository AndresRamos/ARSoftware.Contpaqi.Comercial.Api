using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class ProductoFactory
{
    public const string Codigo = "PROD001";
    public const string Nombre = "Producto 1";
    public const string ClaveSat = "43231500";

    public static CrearProductoRequest Crear()
    {
        var request = new CrearProductoRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Producto.Codigo = Codigo;
        request.Model.Producto.Nombre = Nombre;
        request.Model.Producto.ClaveSat = ClaveSat;

        request.Model.Producto.DatosExtra = GetDatosExtra();

        return request;
    }

    public static ActualizarProductoRequest Actualizar()
    {
        var request = new ActualizarProductoRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.CodigoProducto = Codigo;
        request.Model.DatosProducto = GetDatosExtra();

        return request;
    }

    public static BuscarProductosRequest BuscarPorId()
    {
        var request = new BuscarProductosRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Id = 100;

        return request;
    }

    public static BuscarProductosRequest BuscarPorCodigo()
    {
        var request = new BuscarProductosRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Codigo = Codigo;

        return request;
    }

    public static BuscarProductosRequest BuscarTodo()
    {
        var request = new BuscarProductosRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        return request;
    }

    public static EliminarProductoRequest Eliminar()
    {
        var request = new EliminarProductoRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

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

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearProductoRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarProductoRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Actualizar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarProductosRequest)}_PorId.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarProductosRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarProductosRequest)}_Todo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarTodo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarProductoRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Eliminar(), options));
    }
}
