using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Agentes.ActualizarAgente;

public sealed class ActualizarAgenteRequestHandler : IRequestHandler<ActualizarAgenteRequest, ActualizarAgenteResponse>
{
    private readonly IAgenteRepository _agenteRepository;
    private readonly IAgenteService _agenteService;

    public ActualizarAgenteRequestHandler(IAgenteService agenteService, IAgenteRepository agenteRepository)
    {
        _agenteService = agenteService;
        _agenteRepository = agenteRepository;
    }

    public async Task<ActualizarAgenteResponse> Handle(ActualizarAgenteRequest request, CancellationToken cancellationToken)
    {
        _agenteService.Actualizar(request.Model.CodigoAgente, request.Model.DatosAgente);

        Agente agente = await _agenteRepository.BuscarPorCodigoAsync(request.Model.CodigoAgente, request.Options, cancellationToken) ??
                        throw new InvalidOperationException();

        return ActualizarAgenteResponse.CreateInstance(agente);
    }
}
