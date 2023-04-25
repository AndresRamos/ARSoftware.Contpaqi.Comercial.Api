using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Agentes.CrearAgente;

public sealed class CrearAgenteRequestHandler : IRequestHandler<CrearAgenteRequest, ApiResponse>
{
    private readonly IAgenteRepository _agenteRepository;
    private readonly IAgenteService _agenteService;
    private readonly ILogger _logger;

    public CrearAgenteRequestHandler(IAgenteService agenteService, IAgenteRepository agenteRepository,
        ILogger<CrearAgenteRequestHandler> logger)
    {
        _agenteService = agenteService;
        _agenteRepository = agenteRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(CrearAgenteRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var datosAgente = new Dictionary<string, string>(request.Model.Agente.DatosExtra);

            datosAgente.TryAdd(nameof(admAgentes.CCODIGOAGENTE), request.Model.Agente.Codigo);

            datosAgente.TryAdd(nameof(admAgentes.CNOMBREAGENTE), request.Model.Agente.Nombre);

            datosAgente.TryAdd(nameof(admAgentes.CTIPOAGENTE), TipoAgenteHelper.ConvertToSdkValue(request.Model.Agente.Tipo).ToString());

            datosAgente.TryAdd(nameof(admAgentes.CFECHAALTAAGENTE), DateTime.Today.ToSdkFecha());

            int agenteId = _agenteService.Crear(datosAgente);

            return ApiResponse.CreateSuccessfull<CrearAgenteResponse, CrearAgenteResponseModel>(new CrearAgenteResponseModel
            {
                Agente = (await _agenteRepository.BuscarPorIdAsync(agenteId, request.Options, cancellationToken))!
            });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al crear el agente.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
