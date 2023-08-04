using System.Text.Json;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums.CatalogosCfdi;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class DocumentoFactory
{
    public const string CodigoConcepto = "400";

    private static CrearDocumentoRequest Crear()
    {
        var request = new CrearDocumentoRequest(new CrearDocumentoRequestModel(), new CrearDocumentoRequestOptions());

        request.Model.Documento = GetDocumento();
        request.Options.UsarFechaDelDia = true;
        request.Options.BuscarSiguienteFolio = true;
        request.Options.CrearCatalogos = false;

        return request;
    }

    private static CrearFacturaRequest CrearFactura()
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
        request.Options.NombrePlantilla = "Facturav40.rdl";

        return request;
    }

    private static ActualizarDocumentoRequest Actualizar()
    {
        var request = new ActualizarDocumentoRequest(new ActualizarDocumentoRequestModel(), new ActualizarDocumentoRequestOptions());

        request.Model.LlaveDocumento.ConceptoCodigo = CodigoConcepto;
        request.Model.LlaveDocumento.Serie = "FE";
        request.Model.LlaveDocumento.Folio = 123;
        request.Model.DatosDocumento = GetDatosExtra();

        return request;
    }

    private static EliminarDocumentoRequest Eliminar()
    {
        var request = new EliminarDocumentoRequest(new EliminarDocumentoRequestModel(), new EliminarDocumentoRequestOptions());

        request.Model.LlaveDocumento.ConceptoCodigo = CodigoConcepto;
        request.Model.LlaveDocumento.Serie = "FE";
        request.Model.LlaveDocumento.Folio = 123;

        return request;
    }

    private static TimbrarDocumentoRequest Timbrar()
    {
        var request = new TimbrarDocumentoRequest(new TimbrarDocumentoRequestModel(), new TimbrarDocumentoRequestOptions());

        request.Model.LlaveDocumento.ConceptoCodigo = CodigoConcepto;
        request.Model.LlaveDocumento.Serie = "FE";
        request.Model.LlaveDocumento.Folio = 123;
        request.Model.ContrasenaCertificado = "12345678a";

        return request;
    }

    private static GenerarDocumentoDigitalRequest GenerarDocumentoDigital()
    {
        var request = new GenerarDocumentoDigitalRequest(new GenerarDocumentoDigitalRequestModel(),
            new GenerarDocumentoDigitalRequestOptions());

        request.Model.LlaveDocumento.ConceptoCodigo = CodigoConcepto;
        request.Model.LlaveDocumento.Serie = "FE";
        request.Model.LlaveDocumento.Folio = 123;

        request.Options.Tipo = TipoArchivoDigital.Pdf;
        request.Options.NombrePlantilla = "Facturav40.rdl";

        return request;
    }

    private static SaldarDocumentoRequest Saldar()
    {
        var request = new SaldarDocumentoRequest(new SaldarDocumentoRequestModel(), new SaldarDocumentoRequestOptions());

        request.Model.DocumentoAPagar.ConceptoCodigo = CodigoConcepto;
        request.Model.DocumentoAPagar.Serie = "FE";
        request.Model.DocumentoAPagar.Folio = 123;

        request.Model.DocumentoPago.ConceptoCodigo = "13";
        request.Model.DocumentoPago.Serie = "";
        request.Model.DocumentoPago.Folio = 456;

        request.Model.Fecha = DateTime.Today;
        request.Model.Importe = 116;

        return request;
    }

    private static CancelarDocumentoRequest Cancelar()
    {
        var request = new CancelarDocumentoRequest(new CancelarDocumentoRequestModel(), new CancelarDocumentoRequestOptions());

        request.Model.LlaveDocumento.ConceptoCodigo = CodigoConcepto;
        request.Model.LlaveDocumento.Serie = "FE";
        request.Model.LlaveDocumento.Folio = 123;
        request.Model.ContrasenaCertificado = "12345678a";
        request.Model.MotivoCancelacion = "1";
        request.Model.Uuid = Guid.NewGuid().ToString();

        return request;
    }

    private static Dictionary<string, string> GetDatosExtra()
    {
        return new Dictionary<string, string>
        {
            { nameof(admDocumentos.COBSERVACIONES), "Observaciones del documento." },
            { nameof(admDocumentos.CTEXTOEXTRA1), "Texto Extra" }
        };
    }

    private static BuscarDocumentosRequest BuscarPorSql()
    {
        var request = new BuscarDocumentosRequest(new BuscarDocumentosRequestModel(), new BuscarDocumentosRequestOptions());

        request.Model.SqlQuery = @"CPENDIENTE > 0.00";

        return request;
    }

    private static BuscarDocumentosRequest BuscarPorRangoFecha()
    {
        var request = new BuscarDocumentosRequest(new BuscarDocumentosRequestModel(), new BuscarDocumentosRequestOptions());

        request.Model.FechaInicio = DateOnly.FromDateTime(DateTime.Today);
        request.Model.FechaFin = DateOnly.FromDateTime(DateTime.Today);

        return request;
    }

    private static BuscarDocumentosRequest BuscarPorId()
    {
        var request = new BuscarDocumentosRequest(new BuscarDocumentosRequestModel(), new BuscarDocumentosRequestOptions());

        request.Model.Id = 1;

        return request;
    }

    private static BuscarDocumentosRequest BuscarPorLlave()
    {
        var request = new BuscarDocumentosRequest(new BuscarDocumentosRequestModel(), new BuscarDocumentosRequestOptions());

        request.Model.Llave = new LlaveDocumento { ConceptoCodigo = "400", Serie = "FACT", Folio = 1 };

        return request;
    }

    private static BuscarDocumentosRequest BuscarPorConcepto()
    {
        var request = new BuscarDocumentosRequest(new BuscarDocumentosRequestModel(), new BuscarDocumentosRequestOptions());

        request.Model.ConceptoCodigo = "400";

        return request;
    }

    private static BuscarDocumentosRequest BuscarPorCliente()
    {
        var request = new BuscarDocumentosRequest(new BuscarDocumentosRequestModel(), new BuscarDocumentosRequestOptions());

        request.Model.ClienteCodigo = "CTE001";

        return request;
    }

    private static Documento GetDocumento()
    {
        var documento = new Documento();
        documento.Fecha = DateTime.Today;
        documento.Concepto.Codigo = CodigoConcepto;
        documento.Cliente = ClienteFactory.CrearClientePrueba();
        documento.Moneda = MonedaEnum.PesoMexicano.ToMoneda();
        documento.TipoCambio = 1;
        documento.Referencia = "Referencia doc";
        documento.Observaciones = "Observaciones del documento.";
        documento.Agente = AgenteFactory.CrearAgenteDatosMinimos();
        documento.FormaPago = FormaPagoEnum._01;
        documento.MetodoPago = MetodoPagoEnum.PPD;
        documento.Movimientos.Add(new Movimiento
        {
            Producto = ProductoFactory.CrearProductoPrueba(),
            Unidades = 1,
            Precio = 100,
            Almacen = new Almacen { Codigo = "1", Nombre = "Almacen Uno" },
            Referencia = "Referencia mov",
            Observaciones = "Observaciones del movimiento"
        });
        documento.DatosExtra = GetDatosExtra();

        return documento;
    }

    public static void CearJson(string directory)
    {
        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearFacturaRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(CrearFactura(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Actualizar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Eliminar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(TimbrarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Timbrar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(GenerarDocumentoDigitalRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GenerarDocumentoDigital(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(SaldarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Saldar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CancelarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(Cancelar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorId.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorLlave.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorLlave(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorConcepto.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorConcepto(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorCliente.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorCliente(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorRangoFecha.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorRangoFecha(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorSql.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(BuscarPorSql(), options));
    }
}
