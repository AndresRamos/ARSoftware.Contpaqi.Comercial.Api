using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Documentos.CrearFactura;

public class CrearFacturaRequestHandler : IRequestHandler<CrearFacturaRequest, CrearFacturaResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IContpaqiSdk _sdk;
    private LlaveDocumento? _llaveDocumento;

    public CrearFacturaRequestHandler(IMapper mapper, IDocumentoRepository documentoRepository, IMediator mediator, IContpaqiSdk sdk)
    {
        _mapper = mapper;
        _documentoRepository = documentoRepository;
        _mediator = mediator;
        _sdk = sdk;
    }

    public async Task<CrearFacturaResponse> Handle(CrearFacturaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var crearDocumentoRequest = new CrearDocumentoRequest(new CrearDocumentoRequestModel { Documento = request.Model.Documento },
                new CrearDocumentoRequestOptions
                {
                    UsarFechaDelDia = request.Options.UsarFechaDelDia,
                    BuscarSiguienteFolio = request.Options.BuscarSiguienteFolio,
                    CrearCatalogos = request.Options.CrearCatalogos
                });

            CrearDocumentoResponse crearDocumentoResponse = await _mediator.Send(crearDocumentoRequest, cancellationToken);

            _llaveDocumento = _mapper.Map<LlaveDocumento>(crearDocumentoResponse.Model.Documento);

            if (request.Options.Timbrar)
            {
                var timbrarDocumentoRequest = new TimbrarDocumentoRequest(
                    new TimbrarDocumentoRequestModel
                    {
                        LlaveDocumento = _llaveDocumento, ContrasenaCertificado = request.Options.ContrasenaCertificado
                    },
                    new TimbrarDocumentoRequestOptions
                    {
                        AgregarArchivo = request.Options.AgregarArchivo,
                        ContenidoArchivo = request.Options.NombreArchivo,
                        NombreArchivo = request.Options.NombreArchivo
                    });
                timbrarDocumentoRequest.Model.LlaveDocumento = _llaveDocumento;
                timbrarDocumentoRequest.Model.ContrasenaCertificado = request.Options.ContrasenaCertificado;
                timbrarDocumentoRequest.Options.AgregarArchivo = request.Options.AgregarArchivo;
                timbrarDocumentoRequest.Options.NombreArchivo = request.Options.NombreArchivo;
                timbrarDocumentoRequest.Options.ContenidoArchivo = request.Options.ContenidoArchivo;

                await _mediator.Send(timbrarDocumentoRequest, cancellationToken);
            }

            DocumentoDigital? xml = null;
            DocumentoDigital? pdf = null;

            if (request.Options.GenerarDocumentosDigitales)
            {
                var generarXmlRequest = new GenerarDocumentoDigitalRequest(
                    new GenerarDocumentoDigitalRequestModel { LlaveDocumento = _llaveDocumento },
                    new GenerarDocumentoDigitalRequestOptions { Tipo = TipoArchivoDigital.Xml });
                GenerarDocumentoDigitalResponse generarXmlResponse = await _mediator.Send(generarXmlRequest, cancellationToken);

                xml = generarXmlResponse.Model.DocumentoDigital;

                if (request.Options.GenerarPdf)
                {
                    var generarPdfRequest = new GenerarDocumentoDigitalRequest(
                        new GenerarDocumentoDigitalRequestModel { LlaveDocumento = _llaveDocumento },
                        new GenerarDocumentoDigitalRequestOptions
                        {
                            Tipo = TipoArchivoDigital.Xml, NombrePlantilla = request.Options.NombrePlantilla
                        });
                    GenerarDocumentoDigitalResponse generarPdfResponse = await _mediator.Send(generarPdfRequest, cancellationToken);
                    pdf = generarPdfResponse.Model.DocumentoDigital;
                }
            }

            return CrearFacturaResponse.CreateInstance(
                await _documentoRepository.BuscarPorLlaveAsync(_llaveDocumento, request.Options, cancellationToken) ??
                throw new InvalidOperationException(), xml, pdf);
        }
        finally
        {
            if (_llaveDocumento is not null)
            {
                var tllave = _mapper.Map<tLlaveDoc>(_llaveDocumento);
                _sdk.fBuscaDocumento(ref tllave).ToResultadoSdk(_sdk).ThrowIfError();
                _sdk.fDesbloqueaDocumento().ToResultadoSdk(_sdk).ThrowIfError();
            }
        }
    }
}
