using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Agentes.BuscarAgentes;

public sealed class BuscarAgentesRequestHandler : IRequestHandler<BuscarAgentesRequest, BuscarAgentesResponse>
{
    private readonly IAgenteRepository _agenteRepository;

    public BuscarAgentesRequestHandler(IAgenteRepository agenteRepository)
    {
        _agenteRepository = agenteRepository;
    }

    public async Task<BuscarAgentesResponse> Handle(BuscarAgentesRequest request, CancellationToken cancellationToken)
    {
        List<Agente> agentes = (await _agenteRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken))
            .ToList();

        return BuscarAgentesResponse.CreateInstance(agentes);
    }
}
