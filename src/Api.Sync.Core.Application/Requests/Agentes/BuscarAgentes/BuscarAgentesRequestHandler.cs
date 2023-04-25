using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Agentes.BuscarAgentes;

public sealed class BuscarAgentesRequestHandler : IRequestHandler<BuscarAgentesRequest, ApiResponse>
{
    private readonly IAgenteRepository _agenteRepository;
    private readonly ILogger _logger;

    public BuscarAgentesRequestHandler(IAgenteRepository agenteRepository, ILogger<BuscarAgentesRequestHandler> logger)
    {
        _agenteRepository = agenteRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(BuscarAgentesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Agente> agentes = (await _agenteRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken))
                .ToList();

            return ApiResponse.CreateSuccessfull<BuscarAgentesResponse, BuscarAgentesResponseModel>(
                new BuscarAgentesResponseModel { Agentes = agentes });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar agentes.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
