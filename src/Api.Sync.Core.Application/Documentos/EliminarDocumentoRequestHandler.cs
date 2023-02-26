using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Documentos;

public sealed class EliminarDocumentoRequestHandler : IRequestHandler<EliminarDocumentoRequest, ApiResponseBase>
{
    private readonly IDocumentoService _documentoService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public EliminarDocumentoRequestHandler(IDocumentoService documentoService,
                                           ILogger<EliminarDocumentoRequestHandler> logger,
                                           IMapper mapper)
    {
        _documentoService = documentoService;
        _logger = logger;
        _mapper = mapper;
    }

    public Task<ApiResponseBase> Handle(EliminarDocumentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _documentoService.Eliminar(_mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento));

            return Task.FromResult<ApiResponseBase>(
                ApiResponseFactory.CreateSuccessfull<EliminarDocumentoResponse, EliminarDocumentoResponseModel>(request.Id,
                    new EliminarDocumentoResponseModel()));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al eliminar el documento.");
            return Task.FromResult<ApiResponseBase>(ApiResponseFactory.CreateFailed<EliminarDocumentoResponse>(request.Id, e.Message));
        }
    }
}
