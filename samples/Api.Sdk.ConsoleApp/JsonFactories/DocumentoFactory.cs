using System.Text.Json;
using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using Almacen = Api.Core.Domain.Models.Almacen;
using Documento = Api.Core.Domain.Models.Documento;
using Movimiento = Api.Core.Domain.Models.Movimiento;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class DocumentoFactory
{
    public const string CodigoConcepto = "400";

    public static CrearDocumentoRequest Crear()
    {
        var request = new CrearDocumentoRequest();

        request.Model.Documento = GetDocumento();
        request.Options.UsarFechaDelDia = true;
        request.Options.BuscarSiguienteFolio = true;
        request.Options.CrearCatalogos = false;

        return request;
    }

    public static CrearFacturaRequest CrearFactura()
    {
        var request = new CrearFacturaRequest();

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

    public static ActualizarDocumentoRequest Actualizar()
    {
        var request = new ActualizarDocumentoRequest();

        request.Model.LlaveDocumento.ConceptoCodigo = CodigoConcepto;
        request.Model.LlaveDocumento.Serie = "FE";
        request.Model.LlaveDocumento.Folio = 123;
        request.Model.DatosDocumento = GetDatosExtra();

        return request;
    }

    public static EliminarDocumentoRequest Eliminar()
    {
        var request = new EliminarDocumentoRequest();

        request.Model.LlaveDocumento.ConceptoCodigo = CodigoConcepto;
        request.Model.LlaveDocumento.Serie = "FE";
        request.Model.LlaveDocumento.Folio = 123;

        return request;
    }

    public static TimbrarDocumentoRequest Timbrar()
    {
        var request = new TimbrarDocumentoRequest();

        request.Model.LlaveDocumento.ConceptoCodigo = CodigoConcepto;
        request.Model.LlaveDocumento.Serie = "FE";
        request.Model.LlaveDocumento.Folio = 123;
        request.Model.ContrasenaCertificado = "12345678a";

        return request;
    }

    public static GenerarDocumentoDigitalRequest GenerarDocumentoDigital()
    {
        var request = new GenerarDocumentoDigitalRequest();

        request.Model.LlaveDocumento.ConceptoCodigo = CodigoConcepto;
        request.Model.LlaveDocumento.Serie = "FE";
        request.Model.LlaveDocumento.Folio = 123;

        request.Options.Tipo = TipoArchivoDigital.Pdf;
        request.Options.NombrePlantilla = "Facturav40.rdl";

        return request;
    }

    public static SaldarDocumentoRequest Saldar()
    {
        var request = new SaldarDocumentoRequest();

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

    public static CancelarDocumentoRequest Cancelar()
    {
        var request = new CancelarDocumentoRequest();

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

    public static BuscarDocumentosRequest BuscarPorSql()
    {
        var request = new BuscarDocumentosRequest();

        request.Model.SqlQuery = @"CPENDIENTE > 0.00";

        return request;
    }

    public static BuscarDocumentosRequest BuscarPorRangoFecha()
    {
        var request = new BuscarDocumentosRequest();

        request.Model.FechaInicio = DateOnly.FromDateTime(DateTime.Today);
        request.Model.FechaFin = DateOnly.FromDateTime(DateTime.Today);

        return request;
    }

    public static BuscarDocumentosRequest BuscarPorId()
    {
        var request = new BuscarDocumentosRequest();

        request.Model.Id = 1;

        return request;
    }

    public static BuscarDocumentosRequest BuscarPorLlave()
    {
        var request = new BuscarDocumentosRequest();

        request.Model.Llave = new LlaveDocumento { ConceptoCodigo = "400", Serie = "FACT", Folio = 1 };

        return request;
    }

    public static BuscarDocumentosRequest BuscarPorConcepto()
    {
        var request = new BuscarDocumentosRequest();

        request.Model.ConceptoCodigo = "400";

        return request;
    }

    public static BuscarDocumentosRequest BuscarPorCliente()
    {
        var request = new BuscarDocumentosRequest();

        request.Model.ClienteCodigo = "CTE001";

        return request;
    }

    private static Documento GetDocumento()
    {
        var documento = new Documento();
        documento.Fecha = DateTime.Today;
        documento.Concepto.Codigo = CodigoConcepto;
        documento.Cliente = ClienteFactory.Crear().Model.Cliente;
        documento.Moneda = Moneda.PesoMexicano;
        documento.TipoCambio = 1;
        documento.Referencia = "Referencia doc";
        documento.Observaciones = "Observaciones del documento.";
        documento.Agente = AgenteFactory.Crear().Model.Agente;
        documento.FormaPago = FormaPago._01;
        documento.MetodoPago = MetodoPago.PPD;
        documento.Movimientos.Add(new Movimiento
        {
            Producto = ProductoFactory.Crear().Model.Producto,
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
        JsonSerializerOptions options = JsonExtensions.GetJsonSerializerOptions();
        options.WriteIndented = true;

        Directory.CreateDirectory(directory);

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearDocumentoRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Crear(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearFacturaRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(CrearFactura(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Actualizar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Eliminar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(TimbrarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Timbrar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(GenerarDocumentoDigitalRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(GenerarDocumentoDigital(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(SaldarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Saldar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CancelarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(Cancelar(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorId.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorId(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorLlave.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorLlave(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorConcepto.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorConcepto(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorCliente.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorCliente(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorRangoFecha.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorRangoFecha(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}_PorSql.json"),
            JsonSerializer.Serialize<IContpaqiRequest>(BuscarPorSql(), options));
    }
}
