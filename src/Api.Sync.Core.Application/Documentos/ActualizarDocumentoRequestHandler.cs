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

public sealed class ActualizarDocumentoRequestHandler : IRequestHandler<ActualizarDocumentoRequest, ApiResponseBase>
{
    private readonly IDocumentoRepository _documentoRepository;
    private readonly IDocumentoService _documentoService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public ActualizarDocumentoRequestHandler(IDocumentoService documentoService,
                                             IDocumentoRepository documentoRepository,
                                             ILogger<ActualizarDocumentoRequestHandler> logger,
                                             IMapper mapper)
    {
        _documentoService = documentoService;
        _documentoRepository = documentoRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ApiResponseBase> Handle(ActualizarDocumentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _documentoService.Actualizar(_mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento), request.Model.DatosDocumento);

            Documento documento =
                (await _documentoRepository.BuscarPorLlaveAsync(request.Model.LlaveDocumento, request.Options, cancellationToken))!;

            return ApiResponseFactory.CreateSuccessfull<ActualizarDocumentoResponse, ActualizarDocumentoResponseModel>(request.Id,
                new ActualizarDocumentoResponseModel { Documento = documento });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al actualizar el documento.");
            return ApiResponseFactory.CreateFailed<ActualizarDocumentoResponse>(request.Id, e.Message);
        }
    }
}
