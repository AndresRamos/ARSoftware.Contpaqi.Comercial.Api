using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Documentos;

public sealed class CancelarDocumentoRequestHandler : IRequestHandler<CancelarDocumentoRequest, ApiResponseBase>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public CancelarDocumentoRequestHandler(IDocumentoService documentoService,
                                           IDocumentoRepository documentoRepository,
                                           ILogger<CancelarDocumentoRequestHandler> logger,
                                           IMapper mapper)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ApiResponseBase> Handle(CancelarDocumentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            int documentoId =
                await _documentoRepository.BusarIdPorLlaveAsync(request.Model.LlaveDocumento, request.Options, cancellationToken);

            // Todo: Cancelar documento por llave para eliminar BusarIdPorLlaveAsync
            if (request.Options.Administrativamente)
                _documentoService.CancelarAdministrativamente(_mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento));
            else
                _documentoService.Cancelar(documentoId,
                    request.Model.ContrasenaCertificado,
                    request.Model.MotivoCancelacion,
                    request.Model.Uuid);

            Documento documento =
                (await _documentoRepository.BuscarPorLlaveAsync(request.Model.LlaveDocumento, request.Options, cancellationToken))!;

            return ApiResponseFactory.CreateSuccessfull<CancelarDocumentoResponse, CancelarDocumentoResponseModel>(request.Id,
                new CancelarDocumentoResponseModel { Documento = documento });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al cancelar el documento.");
            return ApiResponseFactory.CreateFailed<CancelarDocumentoResponse>(request.Id, e.Message);
        }
    }
}
