using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.UnidadesMedida;

public sealed class CrearUnidadMedidaRequestHandler : IRequestHandler<CrearUnidadMedidaRequest, CrearUnidadMedidaResponse>
{
    private readonly IUnidadMedidaRepository _unidadMedidaRepository;
    private readonly IUnidadMedidaService _unidadMedidaService;

    public CrearUnidadMedidaRequestHandler(IUnidadMedidaService unidadMedidaService, IUnidadMedidaRepository unidadMedidaRepository)
    {
        _unidadMedidaService = unidadMedidaService;
        _unidadMedidaRepository = unidadMedidaRepository;
    }

    public Task<CrearUnidadMedidaResponse> Handle(CrearUnidadMedidaRequest request, CancellationToken cancellationToken)
    {
        int unidadMedidaId = _unidadMedidaService.Crear(request.Model.UnidadMedida);

        UnidadMedida? unidadMedida = _unidadMedidaRepository.BuscarPorId(unidadMedidaId) ?? throw new InvalidOperationException();

        return Task.FromResult(CrearUnidadMedidaResponse.CreateInstance(unidadMedida));
    }
}
