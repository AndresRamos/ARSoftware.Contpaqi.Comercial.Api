using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Documentos.EliminarDocumento;

public sealed class EliminarDocumentoRequestHandler : IRequestHandler<EliminarDocumentoRequest, ApiResponse>
{
    private readonly IDocumentoService _documentoService;
    private readonly ILogger _logger;
    private readonly IMapper _mapper;

    public EliminarDocumentoRequestHandler(IDocumentoService documentoService, ILogger<EliminarDocumentoRequestHandler> logger,
        IMapper mapper)
    {
        _documentoService = documentoService;
        _logger = logger;
        _mapper = mapper;
    }

    public Task<ApiResponse> Handle(EliminarDocumentoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _documentoService.Eliminar(_mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento));

            return Task.FromResult(
                ApiResponse.CreateSuccessfull<EliminarDocumentoResponse, EliminarDocumentoResponseModel>(
                    new EliminarDocumentoResponseModel()));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al eliminar el documento.");
            return Task.FromResult(ApiResponse.CreateFailed(e.Message));
        }
    }
}
