using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public sealed class AlmacenFactory
{
    public const string Codigo = "ALM001";
    public const string Nombre = "Almacen 1";

    public static CrearAlmacenRequest Crear()
    {
        var request = new CrearAlmacenRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Almacen.Codigo = Codigo;
        request.Model.Almacen.Nombre = Nombre;
        request.Model.Almacen.DatosExtra = GetDatosExtra();

        return request;
    }

    public static ActualizarAlmacenRequest Actualizar()
    {
        var request = new ActualizarAlmacenRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.CodigoAlmacen = Codigo;
        request.Model.DatosAlmacen = GetDatosExtra();

        return request;
    }

    public static BuscarAlmacenesRequest BuscarPorId()
    {
        var request = new BuscarAlmacenesRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Id = 100;

        return request;
    }

    public static BuscarAlmacenesRequest BuscarPorCodigo()
    {
        var request = new BuscarAlmacenesRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        request.Model.Codigo = Codigo;

        return request;
    }

    public static BuscarAlmacenesRequest BuscarTodo()
    {
        var request = new BuscarAlmacenesRequest();
        request.EmpresaRfc = Constants.EmpresaRfc;

        return request;
    }

    private static Dictionary<string, string> GetDatosExtra()
    {
        return new Dictionary<string, string>
        {
            { nameof(admAlmacenes.CFECHAALTAALMACEN), DateTime.Today.ToSdkFecha() },
            { nameof(admAlmacenes.CTEXTOEXTRA1), "Texto Extra" }
        };
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearAlmacenRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarAlmacenRequest)}.json"),
            JsonSerializer.Serialize<ApiRequestBase>(Actualizar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAlmacenesRequest)}_PorId.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAlmacenesRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAlmacenesRequest)}_Todo.json"),
            JsonSerializer.Serialize<ApiRequestBase>(BuscarTodo(), options));
    }
}
