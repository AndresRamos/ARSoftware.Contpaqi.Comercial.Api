using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class AlmacenFactory
{
    public const string Codigo = "ALM001";
    public const string Nombre = "Almacen 1";

    private static CrearAlmacenRequest Crear()
    {
        var request = new CrearAlmacenRequest();

        request.Model.Almacen.Codigo = Codigo;
        request.Model.Almacen.Nombre = Nombre;
        request.Model.Almacen.DatosExtra = GetDatosExtra();

        return request;
    }

    private static ActualizarAlmacenRequest Actualizar()
    {
        var request = new ActualizarAlmacenRequest();

        request.Model.CodigoAlmacen = Codigo;
        request.Model.DatosAlmacen = GetDatosExtra();

        return request;
    }

    private static BuscarAlmacenesRequest BuscarPorId()
    {
        var request = new BuscarAlmacenesRequest();

        request.Model.Id = 100;

        return request;
    }

    private static BuscarAlmacenesRequest BuscarPorCodigo()
    {
        var request = new BuscarAlmacenesRequest();

        request.Model.Codigo = Codigo;

        return request;
    }

    private static BuscarAlmacenesRequest BuscarPorSql()
    {
        var request = new BuscarAlmacenesRequest();

        request.Model.SqlQuery = "CNOMBREALMACEN = 'nombre'";

        return request;
    }

    private static BuscarAlmacenesRequest BuscarTodo()
    {
        var request = new BuscarAlmacenesRequest();

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
        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearAlmacenRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarAlmacenRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Actualizar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAlmacenesRequest)}_PorId.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAlmacenesRequest)}_PorCodigo.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorCodigo(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAlmacenesRequest)}_PorSql.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorSql(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAlmacenesRequest)}_Todo.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarTodo(), options));
    }
}
