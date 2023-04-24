using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Requests.Productos;

public sealed class ActualizarProductoRequestHandler : IRequestHandler<ActualizarProductoRequest, ApiResponse>
{
    private readonly ILogger _logger;
    private readonly IProductoRepository _productoRepository;
    private readonly IProductoService _productoService;

    public ActualizarProductoRequestHandler(IProductoService productoService, IProductoRepository productoRepository,
        ILogger<ActualizarProductoRequestHandler> logger)
    {
        _productoService = productoService;
        _productoRepository = productoRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(ActualizarProductoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _productoService.Actualizar(request.Model.CodigoProducto, request.Model.DatosProducto);

            return ApiResponse.CreateSuccessfull<ActualizarProductoResponse, ActualizarProductoResponseModel>(
                new ActualizarProductoResponseModel
                {
                    Producto = (await _productoRepository.BuscarPorCodigoAsync(request.Model.CodigoProducto, request.Options,
                        cancellationToken))!
                });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al actualizar el producto.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
