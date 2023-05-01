using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Conceptos.BuscarConceptos;

public sealed class BuscarConceptosRequestHandler : IRequestHandler<BuscarConceptosRequest, BuscarConceptosResponse>
{
    private readonly IConceptoRepository _conceptoRepository;

    public BuscarConceptosRequestHandler(IConceptoRepository conceptoRepository)
    {
        _conceptoRepository = conceptoRepository;
    }

    public async Task<BuscarConceptosResponse> Handle(BuscarConceptosRequest request, CancellationToken cancellationToken)
    {
        List<Concepto> conceptos = (await _conceptoRepository.BuscarPorRequstModelAsync(request.Model, request.Options, cancellationToken))
            .ToList();

        return BuscarConceptosResponse.CreateInstance(conceptos);
    }
}
