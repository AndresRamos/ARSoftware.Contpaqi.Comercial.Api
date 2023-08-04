using System.Text.Json;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Api.Common.Domain;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums.CatalogosCfdi;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.ValueObjects;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;

namespace Api.Sdk.ConsoleApp.JsonFactories;

public static class DocumentoFactory
{
    private static readonly LlaveDocumento LlaveFacturaPrueba = new("FACTURAPRUEBA", "FP", 1);

    private static CrearDocumentoRequest GetCrearDocumentoRequest()
    {
        return new CrearDocumentoRequest(new CrearDocumentoRequestModel { Documento = GetModeloPrueba() },
            new CrearDocumentoRequestOptions { UsarFechaDelDia = true, BuscarSiguienteFolio = true, CrearCatalogos = false });
    }

    private static CrearDocumentoResponse GetCrearDocumentoResponse()
    {
        return new CrearDocumentoResponse(new CrearDocumentoResponseModel(GetModeloPrueba()));
    }

    private static CrearFacturaRequest GetCrearFacturaRequest()
    {
        return new CrearFacturaRequest(new CrearFacturaRequestModel { Documento = GetModeloPrueba() },
            new CrearFacturaRequestOptions
            {
                UsarFechaDelDia = true,
                BuscarSiguienteFolio = true,
                CrearCatalogos = false,
                Timbrar = true,
                ContrasenaCertificado = "12345678a",
                GenerarDocumentosDigitales = true,
                GenerarPdf = true,
                NombrePlantilla = "Facturav40.rdl"
            });
    }

    private static CrearFacturaResponse GetCrearFacturaResponse()
    {
        return new CrearFacturaResponse(new CrearFacturaResponseModel(GetModeloPrueba(), new DocumentoDigital(), new DocumentoDigital()));
    }

    private static ActualizarDocumentoRequest GetActualizarDocumentoRequest()
    {
        return new ActualizarDocumentoRequest(
            new ActualizarDocumentoRequestModel { LlaveDocumento = LlaveFacturaPrueba, DatosDocumento = GetDatosExtraPrueba() },
            new ActualizarDocumentoRequestOptions());
    }

    private static ActualizarDocumentoResponse GetActualizarDocumentoResponse()
    {
        return new ActualizarDocumentoResponse(new ActualizarDocumentoResponseModel(GetModeloPrueba()));
    }

    private static EliminarDocumentoRequest GetEliminarDocumentoRequest()
    {
        return new EliminarDocumentoRequest(new EliminarDocumentoRequestModel { LlaveDocumento = LlaveFacturaPrueba },
            new EliminarDocumentoRequestOptions());
    }

    private static EliminarDocumentoResponse GetEliminarDocumentoResponse()
    {
        return new EliminarDocumentoResponse(new EliminarDocumentoResponseModel());
    }

    private static TimbrarDocumentoRequest GetTimbrarDocumentoRequest()
    {
        return new TimbrarDocumentoRequest(
            new TimbrarDocumentoRequestModel { LlaveDocumento = LlaveFacturaPrueba, ContrasenaCertificado = "12345678a" },
            new TimbrarDocumentoRequestOptions());
    }

    private static TimbrarDocumentoResponse GetTimbrarDocumentoResponse()
    {
        return new TimbrarDocumentoResponse(new TimbrarDocumentoResponseModel(GetModeloPrueba()));
    }

    private static GenerarDocumentoDigitalRequest GetGenerarDocumentoDigitalRequest()
    {
        return new GenerarDocumentoDigitalRequest(new GenerarDocumentoDigitalRequestModel { LlaveDocumento = LlaveFacturaPrueba },
            new GenerarDocumentoDigitalRequestOptions { Tipo = TipoArchivoDigital.Pdf, NombrePlantilla = "Facturav40.rdl" });
    }

    private static GenerarDocumentoDigitalResponse GetGenerarDocumentoDigitalResponse()
    {
        return new GenerarDocumentoDigitalResponse(new GenerarDocumentoDigitalResponseModel(new DocumentoDigital()));
    }

    private static SaldarDocumentoRequest GetSaldarDocumentoRequest()
    {
        return new SaldarDocumentoRequest(
            new SaldarDocumentoRequestModel
            {
                DocumentoAPagar = LlaveFacturaPrueba,
                DocumentoPago = new LlaveDocumento { ConceptoCodigo = "13", Serie = "FE", Folio = 456 },
                Fecha = DateTime.Today,
                Importe = 116
            }, new SaldarDocumentoRequestOptions());
    }

    private static SaldarDocumentoResponse GetSaldarDocumentoResponse()
    {
        return new SaldarDocumentoResponse(new SaldarDocumentoResponseModel(new Documento(), new Documento()));
    }

    private static CancelarDocumentoRequest GetCancelarDocumentoRequest()
    {
        return new CancelarDocumentoRequest(
            new CancelarDocumentoRequestModel
            {
                LlaveDocumento = LlaveFacturaPrueba,
                ContrasenaCertificado = "12345678a",
                MotivoCancelacion = "1",
                Uuid = Guid.NewGuid().ToString()
            }, new CancelarDocumentoRequestOptions());
    }

    private static CancelarDocumentoResponse GetCancelarDocumentoResponse()
    {
        return new CancelarDocumentoResponse(new CancelarDocumentoResponseModel(GetModeloPrueba()));
    }

    private static BuscarDocumentosRequest GetBuscarDocumentosRequest()
    {
        return new BuscarDocumentosRequest(
            new BuscarDocumentosRequestModel
            {
                Id = 1,
                Llave = new LlaveDocumento("PRUEBA", "SERIE", 1),
                ConceptoCodigo = "PRUEBA",
                ClienteCodigo = "PRUEBA",
                FechaInicio = DateOnly.FromDateTime(DateTime.Today),
                FechaFin = DateOnly.FromDateTime(DateTime.Today),
                SqlQuery = $"{nameof(admDocumentos.CPENDIENTE)} > 0"
            }, new BuscarDocumentosRequestOptions());
    }

    private static BuscarDocumentosResponse GetBuscarDocumentosResponse()
    {
        return new BuscarDocumentosResponse(new BuscarDocumentosResponseModel(new List<Documento> { GetModeloPrueba() }));
    }

    private static Documento GetModeloPrueba()
    {
        var documento = new Documento();
        documento.Fecha = DateTime.Today;
        documento.Concepto = new ConceptoDocumento { Codigo = ConceptoFactory.CodigoPrueba };
        documento.Cliente = new ClienteProveedor { Codigo = ClienteFactory.CodigoPrueba };
        documento.Moneda = MonedaEnum.PesoMexicano.ToMoneda();
        documento.TipoCambio = 1;
        documento.Referencia = "Referencia doc";
        documento.Observaciones = "Observaciones del documento.";
        documento.Agente = new Agente { Codigo = AgenteFactory.CodigoPrueba };
        documento.FormaPago = FormaPagoEnum._01;
        documento.MetodoPago = MetodoPagoEnum.PPD;
        documento.Movimientos.Add(new Movimiento
        {
            Producto = new Producto { Codigo = ProductoFactory.CodigoPrueba },
            Almacen = new Almacen { Codigo = AlmacenFactory.CodigoPrueba },
            Unidades = 1,
            Precio = 100,
            Impuestos = new ImpuestosMovimiento { Impuesto1 = new Impuesto { Tasa = 16 } },
            Descuentos = new DescuentosMovimiento { Descuento1 = new Descuento { Tasa = 10 } },
            Referencia = "Referencia mov",
            Observaciones = "Observaciones del movimiento"
        });
        documento.DatosExtra = GetDatosExtraPrueba();

        return documento;
    }

    private static Dictionary<string, string> GetDatosExtraPrueba()
    {
        return new Dictionary<string, string>
        {
            { nameof(admDocumentos.COBSERVACIONES), "Observaciones del documento." },
            { nameof(admDocumentos.CTEXTOEXTRA1), "Texto extra 1" },
            { nameof(admDocumentos.CTEXTOEXTRA2), "Texto extra 2" },
            { nameof(admDocumentos.CTEXTOEXTRA3), "Texto extra 3" }
        };
    }

    public static void CearJson(string directory)
    {
        Directory.CreateDirectory(directory);

        JsonSerializerOptions options = FactoryExtensions.GetJsonSerializerOptions();

        File.WriteAllText(Path.Combine(directory, $"{nameof(Documento)}.json"), JsonSerializer.Serialize(GetModeloPrueba(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetCrearDocumentoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearDocumentoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetCrearDocumentoResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearFacturaRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetCrearFacturaRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(CrearFacturaResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetCrearFacturaResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetActualizarDocumentoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(ActualizarDocumentoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetActualizarDocumentoResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetEliminarDocumentoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(EliminarDocumentoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetEliminarDocumentoResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(TimbrarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetTimbrarDocumentoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(TimbrarDocumentoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetTimbrarDocumentoResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(GenerarDocumentoDigitalRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetGenerarDocumentoDigitalRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(GenerarDocumentoDigitalResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetGenerarDocumentoDigitalResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(SaldarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetSaldarDocumentoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(SaldarDocumentoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetSaldarDocumentoResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(CancelarDocumentoRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetCancelarDocumentoRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(CancelarDocumentoResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetCancelarDocumentoResponse(), options));

        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosRequest)}.json"),
            JsonSerializer.Serialize<ContpaqiRequest>(GetBuscarDocumentosRequest(), options));
        File.WriteAllText(Path.Combine(directory, $"{nameof(BuscarDocumentosResponse)}.json"),
            JsonSerializer.Serialize<ContpaqiResponse>(GetBuscarDocumentosResponse(), options));
    }
}
