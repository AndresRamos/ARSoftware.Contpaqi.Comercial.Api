using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using AutoMapper;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Documentos.EliminarDocumento;

public sealed class EliminarDocumentoRequestHandler : IRequestHandler<EliminarDocumentoRequest, EliminarDocumentoResponse>
{
    private readonly IDocumentoService _documentoService;
    private readonly IMapper _mapper;

    public EliminarDocumentoRequestHandler(IDocumentoService documentoService, IMapper mapper)
    {
        _documentoService = documentoService;
        _mapper = mapper;
    }

    public Task<EliminarDocumentoResponse> Handle(EliminarDocumentoRequest request, CancellationToken cancellationToken)
    {
        _documentoService.Eliminar(_mapper.Map<tLlaveDoc>(request.Model.LlaveDocumento));

        return Task.FromResult(EliminarDocumentoResponse.CreateInstance());
    }
}
