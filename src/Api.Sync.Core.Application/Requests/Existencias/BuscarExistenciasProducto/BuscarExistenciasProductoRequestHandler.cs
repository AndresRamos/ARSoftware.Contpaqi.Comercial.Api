using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Existencias.BuscarExistenciasProducto;

public sealed class
    BuscarExistenciasProductoRequestHandler : IRequestHandler<BuscarExistenciasProductoRequest, BuscarExistenciasProductoResponse>
{
    private readonly IExistenciasProductoRepository _existenciasProductoRepository;

    public BuscarExistenciasProductoRequestHandler(IExistenciasProductoRepository existenciasProductoRepository)
    {
        _existenciasProductoRepository = existenciasProductoRepository;
    }

    public async Task<BuscarExistenciasProductoResponse> Handle(BuscarExistenciasProductoRequest request,
        CancellationToken cancellationToken)
    {
        double existencias = await _existenciasProductoRepository.BuscaExistenciasAsync(request.Model, cancellationToken);

        return BuscarExistenciasProductoResponse.CreateInstance(existencias);
    }
}