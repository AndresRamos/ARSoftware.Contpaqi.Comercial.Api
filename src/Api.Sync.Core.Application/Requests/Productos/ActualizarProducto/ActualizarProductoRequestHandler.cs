using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Productos.ActualizarProducto;

public sealed class ActualizarProductoRequestHandler : IRequestHandler<ActualizarProductoRequest, ActualizarProductoResponse>
{
    private readonly IProductoRepository _productoRepository;
    private readonly IProductoService _productoService;

    public ActualizarProductoRequestHandler(IProductoService productoService, IProductoRepository productoRepository)
    {
        _productoService = productoService;
        _productoRepository = productoRepository;
    }

    public async Task<ActualizarProductoResponse> Handle(ActualizarProductoRequest request, CancellationToken cancellationToken)
    {
        _productoService.Actualizar(request.Model.CodigoProducto, request.Model.DatosProducto);

        Producto producto =
            await _productoRepository.BuscarPorCodigoAsync(request.Model.CodigoProducto, request.Options, cancellationToken) ??
            throw new InvalidOperationException();

        return ActualizarProductoResponse.CreateInstance(producto);
    }
}
