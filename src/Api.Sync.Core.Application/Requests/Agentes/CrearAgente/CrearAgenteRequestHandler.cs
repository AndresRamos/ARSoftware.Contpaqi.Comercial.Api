using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Agentes.CrearAgente;

public sealed class CrearAgenteRequestHandler : IRequestHandler<CrearAgenteRequest, CrearAgenteResponse>
{
    private readonly IAgenteRepository _agenteRepository;
    private readonly IAgenteService _agenteService;

    public CrearAgenteRequestHandler(IAgenteService agenteService, IAgenteRepository agenteRepository)
    {
        _agenteService = agenteService;
        _agenteRepository = agenteRepository;
    }

    public async Task<CrearAgenteResponse> Handle(CrearAgenteRequest request, CancellationToken cancellationToken)
    {
        int agenteId = _agenteService.Crear(request.Model.Agente);

        Agente agente = await _agenteRepository.BuscarPorIdAsync(agenteId, request.Options, cancellationToken) ??
                        throw new InvalidOperationException();

        return CrearAgenteResponse.CreateInstance(agente);
    }
}
