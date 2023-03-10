using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Agentes;

public class CrearAgenteRequestHandler : IRequestHandler<CrearAgenteRequest, ApiResponseBase>
{
    private readonly IAgenteRepository _agenteRepository;
    private readonly IAgenteService _agenteService;
    private readonly ILogger _logger;

    public CrearAgenteRequestHandler(IAgenteService agenteService,
                                     IAgenteRepository agenteRepository,
                                     ILogger<CrearAgenteRequestHandler> logger)
    {
        _agenteService = agenteService;
        _agenteRepository = agenteRepository;
        _logger = logger;
    }

    public async Task<ApiResponseBase> Handle(CrearAgenteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var datosAgente = new Dictionary<string, string>(request.Model.Agente.DatosExtra);

            if (!datosAgente.ContainsKey(nameof(admAgentes.CCODIGOAGENTE)))
                datosAgente.Add(nameof(admAgentes.CCODIGOAGENTE), request.Model.Agente.Codigo);

            if (!datosAgente.ContainsKey(nameof(admAgentes.CNOMBREAGENTE)))
                datosAgente.Add(nameof(admAgentes.CNOMBREAGENTE), request.Model.Agente.Nombre);

            int agenteId = _agenteService.Crear(datosAgente);

            return ApiResponseFactory.CreateSuccessfull<CrearAgenteResponse, CrearAgenteResponseModel>(request.Id,
                new CrearAgenteResponseModel
                {
                    Agente = (await _agenteRepository.BuscarPorIdAsync(agenteId, request.Options, cancellationToken))!
                });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al crear el agente.");
            return ApiResponseFactory.CreateFailed<CrearAgenteResponse>(request.Id, e.Message);
        }
    }
}
