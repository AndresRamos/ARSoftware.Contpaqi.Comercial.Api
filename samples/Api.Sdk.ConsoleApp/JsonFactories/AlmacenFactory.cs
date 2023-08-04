using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class AlmacenFactory
{
    public const string CodigoPrueba = "ALMACENPRUEBA";

    private static CrearAlmacenRequest GetCrearAlmacenRequest()
    {
        return new CrearAlmacenRequest(new CrearAlmacenRequestModel { Almacen = GetModeloPrueba() }, new CrearAlmacenRequestOptions());
    }

    private static CrearAlmacenResponse GetCrearAlmacenResponse()
    {
        return new CrearAlmacenResponse(new CrearAlmacenResponseModel(GetModeloPrueba()));
    }

    private static ActualizarAlmacenRequest GetActualizarAlmacenRequest()
    {
        return new ActualizarAlmacenRequest(
            new ActualizarAlmacenRequestModel { CodigoAlmacen = CodigoPrueba, DatosAlmacen = GetDatosExtraPrueba() },
            new ActualizarAlmacenRequestOptions());
    }

    private static ActualizarAlmacenResponse GetActualizarAlmacenResponse()
    {
        return new ActualizarAlmacenResponse(new ActualizarAlmacenResponseModel(GetModeloPrueba()));
    }

    private static BuscarAlmacenesRequest GetBuscarAlmacenesRequest()
    {
        return new BuscarAlmacenesRequest(
            new BuscarAlmacenesRequestModel
            {
                Id = 1, Codigo = CodigoPrueba, SqlQuery = $"{nameof(admAlmacenes.CNOMBREALMACEN)} = 'ALMACEN DE PRUEBAS'"
            }, new BuscarAlmacenesRequestOptions());
    }

    private static BuscarAlmacenesResponse GetBuscarAlmacenesResponse()
    {
        return new BuscarAlmacenesResponse(new BuscarAlmacenesResponseModel(new List<Almacen> { GetModeloPrueba() }));
    }

    private static Almacen GetModeloPrueba()
    {
        return new Almacen { Codigo = CodigoPrueba, Nombre = "ALMACEN DE PRUEBAS", DatosExtra = GetDatosExtraPrueba() };
    }

    private static Dictionary<string, string> GetDatosExtraPrueba()
    {
        return new Dictionary<string, string>
        {
            { nameof(admAlmacenes.CTEXTOEXTRA1), "Texto extra 1" },
            { nameof(admAlmacenes.CTEXTOEXTRA2), "Texto extra 2" },
            { nameof(admAlmacenes.CTEXTOEXTRA3), "Texto extra 3" }
        };
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(Almacen)}.json"), JsonSerializer.Serialize(GetModeloPrueba(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearAlmacenRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetCrearAlmacenRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearAlmacenResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetCrearAlmacenResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarAlmacenRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetActualizarAlmacenRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarAlmacenResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetActualizarAlmacenResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAlmacenesRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarAlmacenesRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarAlmacenesResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarAlmacenesResponse(), options));
    }
}
