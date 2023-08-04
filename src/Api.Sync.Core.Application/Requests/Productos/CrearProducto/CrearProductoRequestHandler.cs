using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Productos.CrearProducto;

public sealed class CrearProductoRequestHandler : IRequestHandler<CrearProductoRequest, CrearProductoResponse>
{
    private readonly IProductoRepository _productoRepository;
    private readonly IProductoService _productoService;

    public CrearProductoRequestHandler(IProductoService productoService, IProductoRepository productoRepository)
    {
        _productoService = productoService;
        _productoRepository = productoRepository;
    }

    public async Task<CrearProductoResponse> Handle(CrearProductoRequest request, CancellationToken cancellationToken)
    {
        int productoId = _productoService.Crear(request.Model.Producto);

        Producto producto = await _productoRepository.BuscarPorIdAsync(productoId, request.Options, cancellationToken) ??
                            throw new InvalidOperationException();

        return CrearProductoResponse.CreateInstance(producto);
    }
}
