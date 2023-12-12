using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.UnidadesMedida.BuscarUnidadesMedida;

public sealed class BuscarUnidadesMedidaRequestHandler : IRequestHandler<BuscarUnidadesMedidaRequest, BuscarUnidadesMedidaResponse>
{
    private readonly IUnidadMedidaRepository _unidadMedidaRepository;

    public BuscarUnidadesMedidaRequestHandler(IUnidadMedidaRepository unidadMedidaRepository)
    {
        _unidadMedidaRepository = unidadMedidaRepository;
    }

    public Task<BuscarUnidadesMedidaResponse> Handle(BuscarUnidadesMedidaRequest request, CancellationToken cancellationToken)
    {
        List<UnidadMedida> unidadesMedida = _unidadMedidaRepository.BuscarTodo();

        return Task.FromResult(BuscarUnidadesMedidaResponse.CreateInstance(unidadesMedida));
    }
}
