using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Existencias.BuscarExistenciasProductoConCaracteristicas;

public sealed class BuscarExistenciasProductoConCaracteristicasRequestHandler : IRequestHandler<
    BuscarExistenciasProductoConCaracteristicasRequest, BuscarExistenciasProductoConCaracteristicasResponse>
{
    private readonly IExistenciasProductoRepository _existenciasProductoRepository;

    public BuscarExistenciasProductoConCaracteristicasRequestHandler(IExistenciasProductoRepository existenciasProductoRepository)
    {
        _existenciasProductoRepository = existenciasProductoRepository;
    }

    public async Task<BuscarExistenciasProductoConCaracteristicasResponse> Handle(
        BuscarExistenciasProductoConCaracteristicasRequest request, CancellationToken cancellationToken)
    {
        double existenciasProducto =
            await _existenciasProductoRepository.BuscaExistenciasConCaracteristicasAsync(request.Model, cancellationToken);

        return BuscarExistenciasProductoConCaracteristicasResponse.CreateInstance(existenciasProducto);
    }
}