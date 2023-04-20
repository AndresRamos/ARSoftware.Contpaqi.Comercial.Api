using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Productos;

public sealed class BuscarProductosRequestHandler : IRequestHandler<BuscarProductosRequest, ApiResponse>
{
    private readonly ILogger _logger;
    private readonly IProductoRepository _productoRepository;

    public BuscarProductosRequestHandler(IProductoRepository productoRepository, ILogger<BuscarProductosRequestHandler> logger)
    {
        _productoRepository = productoRepository;
        _logger = logger;
    }

    public async Task<ApiResponse> Handle(BuscarProductosRequest request, CancellationToken cancellationToken)
    {
        try
        {
            List<Producto> productos = (await _productoRepository.BuscarPorRequestModel(request.Model, request.Options, cancellationToken))
                .ToList();

            return ApiResponse.CreateSuccessfull<BuscarProductosResponse, BuscarProductosResponseModel>(
                new BuscarProductosResponseModel { Productos = productos });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar los productos.");
            return ApiResponse.CreateFailed(e.Message);
        }
    }
}
