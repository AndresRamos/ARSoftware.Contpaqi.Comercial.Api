using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using AutoMapper;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Productos.CrearProducto;

public sealed class CrearProductoRequestHandler : IRequestHandler<CrearProductoRequest, CrearProductoResponse>
{
    private readonly IMapper _mapper;
    private readonly IProductoRepository _productoRepository;
    private readonly IProductoService _productoService;

    public CrearProductoRequestHandler(IProductoService productoService, IProductoRepository productoRepository, IMapper mapper)
    {
        _productoService = productoService;
        _productoRepository = productoRepository;
        _mapper = mapper;
    }

    public async Task<CrearProductoResponse> Handle(CrearProductoRequest request, CancellationToken cancellationToken)
    {
        int productoId = _productoService.Crear(_mapper.Map<tProducto>(request.Model.Producto));

        var datosExtra = new Dictionary<string, string>(request.Model.Producto.DatosExtra);

        datosExtra.TryAdd(nameof(admProductos.CCLAVESAT), request.Model.Producto.ClaveSat);

        _productoService.Actualizar(productoId, datosExtra);

        Producto producto = await _productoRepository.BuscarPorIdAsync(productoId, request.Options, cancellationToken) ??
                            throw new InvalidOperationException();

        return CrearProductoResponse.CreateInstance(producto);
    }
}
