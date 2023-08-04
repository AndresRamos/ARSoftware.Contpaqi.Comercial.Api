using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Productos.BuscarProductos;

public sealed class BuscarProductosRequestHandler : IRequestHandler<BuscarProductosRequest, BuscarProductosResponse>
{
    private readonly IProductoRepository _productoRepository;

    public BuscarProductosRequestHandler(IProductoRepository productoRepository)
    {
        _productoRepository = productoRepository;
    }

    public async Task<BuscarProductosResponse> Handle(BuscarProductosRequest request, CancellationToken cancellationToken)
    {
        List<Producto> productos = (await _productoRepository.BuscarPorRequestModel(request.Model, request.Options, cancellationToken))
            .ToList();

        return BuscarProductosResponse.CreateInstance(productos);
    }
}
