using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Agentes;

public sealed class BuscarAgentesRequestHandler : IRequestHandler<BuscarAgentesRequest, ApiResponseBase>
{
    private readonly IAgenteRepository _agenteRepository;
    private readonly ILogger _logger;

    public BuscarAgentesRequestHandler(IAgenteRepository agenteRepository, ILogger<BuscarAgentesRequestHandler> logger)
    {
        _agenteRepository = agenteRepository;
        _logger = logger;
    }

    public async Task<ApiResponseBase> Handle(BuscarAgentesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Agente> agentes = (await _agenteRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken))
                .ToList();

            return ApiResponseFactory.CreateSuccessfull<BuscarAgentesResponse, BuscarAgentesResponseModel>(request.Id,
                new BuscarAgentesResponseModel { Agentes = agentes });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar agentes.");
            return ApiResponseFactory.CreateFailed<BuscarAgentesResponse>(request.Id, e.Message);
        }
    }
}
