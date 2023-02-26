using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Productos;

public sealed class BuscarProductosRequestHandler : IRequestHandler<BuscarProductosRequest, ApiResponseBase>
{
    private readonly ILogger _logger;
    private readonly IProductoRepository _productoRepository;

    public BuscarProductosRequestHandler(IProductoRepository productoRepository, ILogger<BuscarProductosRequestHandler> logger)
    {
        _productoRepository = productoRepository;
        _logger = logger;
    }

    public async Task<ApiResponseBase> Handle(BuscarProductosRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var productos = new List<Producto>();

            if (request.Model.Id is not null)
            {
                Producto? producto = await _productoRepository.BuscarPorIdAsync(request.Model.Id.Value, cancellationToken);
                if (producto is not null)
                    productos.Add(producto);
            }
            else if (request.Model.Codigo is not null)
            {
                Producto? producto = await _productoRepository.BuscarPorCodigoAsync(request.Model.Codigo, cancellationToken);
                if (producto is not null)
                    productos.Add(producto);
            }
            else
            {
                productos.AddRange(await _productoRepository.BuscarTodoAsync(cancellationToken));
            }

            return ApiResponseFactory.CreateSuccessfull<BuscarProductosResponse, BuscarProductosResponseModel>(request.Id,
                new BuscarProductosResponseModel { Productos = productos });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al buscar los productos.");
            return ApiResponseFactory.CreateFailed<BuscarProductosResponse>(request.Id, e.Message);
        }
    }
}
