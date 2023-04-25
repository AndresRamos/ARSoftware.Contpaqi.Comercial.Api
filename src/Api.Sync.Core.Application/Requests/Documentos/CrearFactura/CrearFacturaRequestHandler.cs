using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.Common.Extensions;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Documentos.CrearFactura;

public class CrearFacturaRequestHandler : IRequestHandler<CrearFacturaRequest, ApiResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;
    private readonly IContpaqiSdk _sdk;
    private LlaveDocumento? _llaveDocumento;

    public CrearFacturaRequestHandler(IMapper mapper, ILogger<CrearFacturaRequestHandler> logger, IDocumentoRepository documentoRepository,
        IMediator mediator, IContpaqiSdk sdk)
    {
        _mapper = mapper;
        _logger = logger;
        _documentoRepository = documentoRepository;
        _mediator = mediator;
        _sdk = sdk;
    }

    public async Task<ApiResponse> Handle(CrearFacturaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var crearDocumentoRequest = new CrearDocumentoRequest();
            crearDocumentoRequest.Model.Documento = request.Model.Documento;
            crearDocumentoRequest.Options.UsarFechaDelDia = request.Options.UsarFechaDelDia;
            crearDocumentoRequest.Options.CrearCatalogos = request.Options.CrearCatalogos;
            ApiResponse crearDocumentoResponse = await _mediator.Send(crearDocumentoRequest, cancellationToken);
            crearDocumentoResponse.ThrowIfError();

            var responseModel = new CrearFacturaResponseModel();

            _llaveDocumento =
                _mapper.Map<LlaveDocumento>((crearDocumentoResponse.ContpaqiResponse as CrearDocumentoResponse)!.Model.Documento);

            if (request.Options.Timbrar)
            {
                var timbrarDocumentoRequest = new TimbrarDocumentoRequest();
                timbrarDocumentoRequest.Model.LlaveDocumento = _llaveDocumento;
                timbrarDocumentoRequest.Model.ContrasenaCertificado = request.Options.ContrasenaCertificado;
                timbrarDocumentoRequest.Options.AgregarArchivo = request.Options.AgregarArchivo;
                timbrarDocumentoRequest.Options.NombreArchivo = request.Options.NombreArchivo;
                timbrarDocumentoRequest.Options.ContenidoArchivo = request.Options.ContenidoArchivo;
                ApiResponse timbrarDocumentoResponse = await _mediator.Send(timbrarDocumentoRequest, cancellationToken);
                timbrarDocumentoResponse.ThrowIfError();
            }

            responseModel.Documento =
                (await _documentoRepository.BuscarPorLlaveAsync(_llaveDocumento, request.Options, cancellationToken))!;

            if (request.Options.GenerarDocumentosDigitales)
            {
                var generarXmlRequest = new GenerarDocumentoDigitalRequest();
                generarXmlRequest.Model.LlaveDocumento = _llaveDocumento;
                generarXmlRequest.Options.Tipo = TipoArchivoDigital.Xml;
                ApiResponse generarXmlResponse = await _mediator.Send(generarXmlRequest, cancellationToken);

                responseModel.Xml = (generarXmlResponse.ContpaqiResponse as GenerarDocumentoDigitalResponse)!.Model.DocumentoDigital;

                if (request.Options.GenerarPdf)
                {
                    var generarPdfRequest = new GenerarDocumentoDigitalRequest();
                    generarPdfRequest.Model.LlaveDocumento = _llaveDocumento;
                    generarPdfRequest.Options.Tipo = TipoArchivoDigital.Pdf;
                    generarPdfRequest.Options.NombrePlantilla = request.Options.NombrePlantilla;
                    ApiResponse generarPdfResponse = await _mediator.Send(generarPdfRequest, cancellationToken);
                    responseModel.Pdf = (generarPdfResponse.ContpaqiResponse as GenerarDocumentoDigitalResponse)!.Model.DocumentoDigital;
                }
            }

            return ApiResponse.CreateSuccessfull<CrearFacturaResponse, CrearFacturaResponseModel>(responseModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Processing {ApiRequest}", nameof(CrearDocumentoRequest));
            // Todo: Borrar documento si hay error
            return ApiResponse.CreateFailed(e.Message);
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
