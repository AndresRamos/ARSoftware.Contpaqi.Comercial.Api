using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Documentos.BuscarDocumentos;

public sealed record BuscarDocumentosRequestHandler : IRequestHandler<BuscarDocumentosRequest, BuscarDocumentosResponse>
{
    private readonly IDocumentoRepository _documentoRepository;

    public BuscarDocumentosRequestHandler(IDocumentoRepository documentoRepository)
    {
        _documentoRepository = documentoRepository;
    }

    public async Task<BuscarDocumentosResponse> Handle(BuscarDocumentosRequest request, CancellationToken cancellationToken)
    {
        List<Documento> documentos =
            (await _documentoRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken)).ToList();

        return BuscarDocumentosResponse.CreateInstance(documentos);
    }
}
