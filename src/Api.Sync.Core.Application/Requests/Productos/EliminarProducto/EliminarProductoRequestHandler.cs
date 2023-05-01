using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Productos.EliminarProducto;

public sealed class EliminarProductoRequestHandler : IRequestHandler<EliminarProductoRequest, EliminarProductoResponse>
{
    private readonly IProductoService _productoService;

    public EliminarProductoRequestHandler(IProductoService productoService)
    {
        _productoService = productoService;
    }

    public Task<EliminarProductoResponse> Handle(EliminarProductoRequest request, CancellationToken cancellationToken)
    {
        _productoService.Eliminar(request.Model.CodigoProducto);

        return Task.FromResult(EliminarProductoResponse.CreateInstance());
    }
}
