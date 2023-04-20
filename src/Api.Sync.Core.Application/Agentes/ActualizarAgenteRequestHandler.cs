using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Agentes;

public sealed class ActualizarAgenteRequestHandler : IRequestHandler<ActualizarAgenteRequest, ApiResponse>
{
    private readonly IAgenteRepository _agenteRepository;
    private readonly IAgenteService _agenteService;
    private readonly ILogger _logger;

    public ActualizarAgenteRequestHandler(IAgenteService agenteService, IAgenteRepository agenteRepository,
        ILogger<ActualizarAgenteRequestHandler> logger)
    {
        _agenteService = agenteService;
        _agenteRepository = agenteRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(ActualizarAgenteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _agenteService.Actualizar(request.Model.CodigoAgente, request.Model.DatosAgente);

            return ApiResponse.CreateSuccessfull<ActualizarAgenteResponse, ActualizarAgenteResponseModel>(new ActualizarAgenteResponseModel
            {
                Agente = (await _agenteRepository.BuscarPorCodigoAsync(request.Model.CodigoAgente, request.Options, cancellationToken))!
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al actualizar el agente.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
