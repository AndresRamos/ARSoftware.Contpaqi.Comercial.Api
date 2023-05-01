using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Helpers;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
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
        var datosAgente = new Dictionary<string, string>(request.Model.Agente.DatosExtra);

        datosAgente.TryAdd(nameof(admAgentes.CCODIGOAGENTE), request.Model.Agente.Codigo);

        datosAgente.TryAdd(nameof(admAgentes.CNOMBREAGENTE), request.Model.Agente.Nombre);

        datosAgente.TryAdd(nameof(admAgentes.CTIPOAGENTE), TipoAgenteHelper.ConvertToSdkValue(request.Model.Agente.Tipo).ToString());

        datosAgente.TryAdd(nameof(admAgentes.CFECHAALTAAGENTE), DateTime.Today.ToSdkFecha());

        int agenteId = _agenteService.Crear(datosAgente);

        Agente agente = await _agenteRepository.BuscarPorIdAsync(agenteId, request.Options, cancellationToken) ??
                        throw new InvalidOperationException();

        return CrearAgenteResponse.CreateInstance(agente);
    }
}
