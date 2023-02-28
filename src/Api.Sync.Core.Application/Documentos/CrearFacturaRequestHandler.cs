using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.Common.Extensions;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Documentos;

public class CrearFacturaRequestHandler : IRequestHandler<CrearFacturaRequest, ApiResponseBase>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public CrearFacturaRequestHandler(IMapper mapper,
                                      ILogger<CrearFacturaRequestHandler> logger,
                                      IDocumentoRepository documentoRepository,
                                      IMediator mediator)
    {
        _mapper = mapper;
        _logger = logger;
        _documentoRepository = documentoRepository;
        _mediator = mediator;
    }

    public async Task<ApiResponseBase> Handle(CrearFacturaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var crearDocumentoRequest = new CrearDocumentoRequest();
            crearDocumentoRequest.Model.Documento = request.Model.Documento;
            crearDocumentoRequest.Options.UsarFechaDelDia = request.Options.UsarFechaDelDia;
            crearDocumentoRequest.Options.CrearCatalogos = request.Options.CrearCatalogos;
            var crearDocumentoResponse = (await _mediator.Send(crearDocumentoRequest, cancellationToken) as CrearDocumentoResponse)!;
            crearDocumentoResponse.ThrowIfError();

            var responseModel = new CrearFacturaResponseModel();

            var llaveDocumento = _mapper.Map<LlaveDocumento>(crearDocumentoResponse.Model.Documento);

            if (request.Options.Timbrar)
            {
                var timbrarDocumentoRequest = new TimbrarDocumentoRequest();
                timbrarDocumentoRequest.Model.LlaveDocumento = llaveDocumento;
                timbrarDocumentoRequest.Model.ContrasenaCertificado = request.Options.ContrasenaCertificado;
                ApiResponseBase timbrarDocumentoResponse = await _mediator.Send(timbrarDocumentoRequest, cancellationToken);
                timbrarDocumentoResponse.ThrowIfError();
            }

            responseModel.Documento = await _documentoRepository.BuscarPorLlaveAsync(llaveDocumento, cancellationToken);

            if (request.Options.GenerarDocumentosDigitales)
            {
                var generarXmlRequest = new GenerarDocumentoDigitalRequest();
                generarXmlRequest.Model.LlaveDocumento = llaveDocumento;
                generarXmlRequest.Options.Tipo = TipoArchivoDigital.Xml;
                var generarXmlResponse = (await _mediator.Send(generarXmlRequest, cancellationToken) as GenerarDocumentoDigitalResponse)!;

                responseModel.Xml = generarXmlResponse.Model.DocumentoDigital;

                if (request.Options.GenerarPdf)
                {
                    var generarPdfRequest = new GenerarDocumentoDigitalRequest();
                    generarPdfRequest.Model.LlaveDocumento = llaveDocumento;
                    generarPdfRequest.Options.Tipo = TipoArchivoDigital.Pdf;
                    generarPdfRequest.Options.NombrePlantilla = request.Options.NombrePlantilla;
                    var generarPdfResponse =
                        (await _mediator.Send(generarPdfRequest, cancellationToken) as GenerarDocumentoDigitalResponse)!;
                    responseModel.Pdf = generarPdfResponse.Model.DocumentoDigital;
                }
            }

            return ApiResponseFactory.CreateSuccessfull<CrearFacturaResponse, CrearFacturaResponseModel>(request.Id, responseModel);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error Processing {ApiRequest}", nameof(CrearDocumentoRequest));
            // Todo: Borrar documento si hay error
            return ApiResponseFactory.CreateFailed<CrearFacturaResponse>(request.Id, e.Message);
        }
    }
}
