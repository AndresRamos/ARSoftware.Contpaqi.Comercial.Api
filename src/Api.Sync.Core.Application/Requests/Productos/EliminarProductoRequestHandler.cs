using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Productos;

public sealed class EliminarProductoRequestHandler : IRequestHandler<EliminarProductoRequest, ApiResponse>
{
    private readonly ILogger _logger;
    private readonly IProductoService _productoService;

    public EliminarProductoRequestHandler(IProductoService productoService, ILogger<EliminarProductoRequestHandler> logger)
    {
        _productoService = productoService;
        _logger = logger;
    }

    public Task<ApiResponse> Handle(EliminarProductoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _productoService.Eliminar(request.Model.CodigoProducto);
            return Task.FromResult(
                ApiResponse.CreateSuccessfull<EliminarProductoResponse, EliminarProductoResponseModel>(
                    new EliminarProductoResponseModel()));
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al eliminar el producto.");
            return Task.FromResult(ApiResponse.CreateFailed(e.Message));
        }
    }
}
