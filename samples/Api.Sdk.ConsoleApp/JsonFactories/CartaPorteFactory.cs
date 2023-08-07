using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums.CatalogosCfdi;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public sealed class CartaPorteFactory
{
    public const string CodigoConcepto = "402";

    private static CrearFacturaRequest Crear()
    {
        var request = new CrearFacturaRequest(new CrearFacturaRequestModel(), new CrearFacturaRequestOptions());

        request.Model.Documento = GetDocumento();

        request.Options.UsarFechaDelDia = true;
        request.Options.BuscarSiguienteFolio = true;
        request.Options.CrearCatalogos = false;
        request.Options.Timbrar = true;
        request.Options.ContrasenaCertificado = "12345678a";
        request.Options.GenerarDocumentosDigitales = true;
        request.Options.GenerarPdf = true;
        request.Options.NombrePlantilla = "Carta_Porte_CFDI_Ingreso_2.rdl";

        request.Options.AgregarArchivo = true;
        request.Options.NombreArchivo = "CartaPorteEjemplo.ini";
        request.Options.ContenidoArchivo = BuscarArchivoAdicional();

        return request;
    }

    private static Documento GetDocumento()
    {
        var documento = new Documento();
        documento.Fecha = DateTime.Today;
        documento.Concepto.Codigo = CodigoConcepto;
        documento.Cliente = new ClienteProveedor { Codigo = "CTEPORTE01" };
        documento.Moneda = MonedaEnum.PesoMexicano.ToMoneda();
        documento.TipoCambio = 1;
        documento.Referencia = "Referencia doc";
        documento.Observaciones = "Observaciones del documento.";
        documento.Agente = new Agente { Codigo = AgenteFactory.CodigoPrueba };
        documento.FormaPago = FormaPagoEnum._03;
        documento.MetodoPago = MetodoPagoEnum.PPD;
        documento.Movimientos.Add(new Movimiento
        {
            Producto = new Producto { Codigo = "PORTE03" },
            Unidades = 1,
            Precio = 25000,
            Almacen = new Almacen { Codigo = "1", Nombre = "Almacen Uno" },
            Referencia = "Referencia mov",
            Observaciones = "Observaciones del movimiento",
            DatosExtra = new Dictionary<string, string> { { nameof(admMovimientos.CPORCENTAJERETENCION2), "4" } }
        });

        return documento;
    }

    private static string BuscarArchivoAdicional()
    {
        return File.ReadAllText(@"C:\AR Software\Contpaqi Comercial API\CartaPorteEjemplo.ini");
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Crear(), options));
    }
}
