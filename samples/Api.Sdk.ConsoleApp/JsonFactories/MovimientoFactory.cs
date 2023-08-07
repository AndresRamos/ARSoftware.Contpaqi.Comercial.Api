using System.Text.Json;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.ValueObjects;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public class MovimientoFactory
{
    public static Movimiento GetModeloPrueba()
    {
        return new Movimiento
        {
            Producto = new Producto { Codigo = ProductoFactory.CodigoPrueba },
            Almacen = new Almacen { Codigo = AlmacenFactory.CodigoPrueba },
            Unidades = 1,
            Precio = 100,
            Impuestos = new ImpuestosMovimiento { Impuesto1 = new Impuesto { Tasa = 16 } },
            Descuentos = new DescuentosMovimiento { Descuento1 = new Descuento { Tasa = 10 } },
            Retenciones = new RetencionesMovimiento { Retencion1 = new Retencion(), Retencion2 = new Retencion() },
            Referencia = "Referencia mov",
            Observaciones = "Observaciones del movimiento",
            SeriesCapas = new List<SeriesCapas> { new() },
            DatosExtra = GetDatosExtraPrueba()
        };
    }

    public static Dictionary<string, string> GetDatosExtraPrueba()
    {
        return new Dictionary<string, string>
        {
            { nameof(admMovimientos.CTEXTOEXTRA1), "Texto extra 1" },
            { nameof(admMovimientos.CTEXTOEXTRA2), "Texto extra 2" },
            { nameof(admMovimientos.CTEXTOEXTRA3), "Texto extra 3" }
        };
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(Movimiento)}.json"), JsonSerializer.Serialize(GetModeloPrueba(), options));
    }
}
