using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Productos;

public sealed class ActualizarProductoRequestHandler : IRequestHandler<ActualizarProductoRequest, ApiResponseBase>
{
    private readonly ILogger _logger;
    private readonly IProductoRepository _productoRepository;
    private readonly IProductoService _productoService;

    public ActualizarProductoRequestHandler(IProductoService productoService,
                                            IProductoRepository productoRepository,
                                            ILogger<ActualizarProductoRequestHandler> logger)
    {
        _productoService = productoService;
        _productoRepository = productoRepository;
        _logger = logger;
    }

    public async Task<ApiResponseBase> Handle(ActualizarProductoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            _productoService.Actualizar(request.Model.CodigoProducto, request.Model.DatosProducto);

            return ApiResponseFactory.CreateSuccessfull<ActualizarProductoResponse, ActualizarProductoResponseModel>(request.Id,
                new ActualizarProductoResponseModel
                {
                    Producto = (await _productoRepository.BuscarPorCodigoAsync(request.Model.CodigoProducto, cancellationToken))!
                });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al actualizar el producto.");
            return ApiResponseFactory.CreateFailed<ActualizarProductoResponse>(request.Id, e.Message);
        }
    }
}
