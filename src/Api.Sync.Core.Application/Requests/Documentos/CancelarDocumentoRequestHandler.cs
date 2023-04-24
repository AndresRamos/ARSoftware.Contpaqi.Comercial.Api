using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Models.Enums;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Documentos;

public sealed class CancelarDocumentoRequestHandler : IRequestHandler<CancelarDocumentoRequest, ApiResponse>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public CancelarDocumentoRequestHandler(IDocumentoService documentoService, IDocumentoRepository documentoRepository,
        ILogger<CancelarDocumentoRequestHandler> logger, IMapper mapper)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ApiResponse> Handle(CancelarDocumentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            int documentoId =
                await _documentoRepository.BusarIdPorLlaveAsync(request.Model.LlaveDocumento, request.Options, cancellationToken);

            // Todo: Cancelar documento por llave para eliminar BusarIdPorLlaveAsync
            if (request.Options.Administrativamente)
                _documentoService.CancelarAdministrativamente(_mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento));
            else
                _documentoService.Cancelar(documentoId, request.Model.ContrasenaCertificado,
                    MotivoCancelacion.FromClave(request.Model.MotivoCancelacion), request.Model.Uuid);

            Documento documento =
                (await _documentoRepository.BuscarPorLlaveAsync(request.Model.LlaveDocumento, request.Options, cancellationToken))!;

            return ApiResponse.CreateSuccessfull<CancelarDocumentoResponse, CancelarDocumentoResponseModel>(
                new CancelarDocumentoResponseModel { Documento = documento });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al cancelar el documento.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
