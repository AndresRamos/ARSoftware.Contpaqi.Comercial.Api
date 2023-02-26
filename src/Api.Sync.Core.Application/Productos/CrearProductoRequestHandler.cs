using Api.Core.Domain.Common;
using Api.Core.Domain.Factories;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Api.Sync.Core.Application.Productos;

public sealed class CrearProductoRequestHandler : IRequestHandler<CrearProductoRequest, ApiResponseBase>
{
    private readonly ILogger _logger;
    private readonly IMapper _mapper;
    private readonly IProductoRepository _productoRepository;
    private readonly IProductoService _productoService;

    public CrearProductoRequestHandler(IProductoService productoService,
                                       IProductoRepository productoRepository,
                                       ILogger<CrearProductoRequestHandler> logger,
                                       IMapper mapper)
    {
        _productoService = productoService;
        _productoRepository = productoRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ApiResponseBase> Handle(CrearProductoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            int productoId = _productoService.Crear(_mapper.Map<tProducto>(request.Model.Producto));

            _productoService.Actualizar(productoId,
                new Dictionary<string, string>(request.Model.Producto.DatosExtra)
                {
                    { nameof(admProductos.CCLAVESAT), request.Model.Producto.ClaveSat }
                });

            return ApiResponseFactory.CreateSuccessfull<CrearProductoResponse, CrearProductoResponseModel>(request.Id,
                new CrearProductoResponseModel { Producto = (await _productoRepository.BuscarPorIdAsync(productoId, cancellationToken))! });
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error al crear el producto.");
            return ApiResponseFactory.CreateFailed<CrearProductoResponse>(request.Id, e.Message);
        }
    }
}
