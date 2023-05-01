using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Almacenes.CrearAlmacen;

public sealed class CrearAlmacenRequestHandler : IRequestHandler<CrearAlmacenRequest, CrearAlmacenResponse>
{
    private readonly IAlmacenRepository _almacenRepository;
    private readonly IAlmacenService _almacenService;

    public CrearAlmacenRequestHandler(IAlmacenRepository almacenRepository, IAlmacenService almacenService)
    {
        _almacenRepository = almacenRepository;
        _almacenService = almacenService;
    }

    public async Task<CrearAlmacenResponse> Handle(CrearAlmacenRequest request, CancellationToken cancellationToken)
    {
        var datosAlmacen = new Dictionary<string, string>(request.Model.Almacen.DatosExtra);

        datosAlmacen.TryAdd(nameof(admAlmacenes.CCODIGOALMACEN), request.Model.Almacen.Codigo);

        datosAlmacen.TryAdd(nameof(admAlmacenes.CNOMBREALMACEN), request.Model.Almacen.Nombre);

        datosAlmacen.TryAdd(nameof(admAlmacenes.CFECHAALTAALMACEN), DateTime.Today.ToSdkFecha());

        int almacenId = _almacenService.Crear(datosAlmacen);

        Almacen almacen = await _almacenRepository.BuscarPorIdAsync(almacenId, request.Options, cancellationToken) ??
                          throw new InvalidOperationException();

        return CrearAlmacenResponse.CreateInstance(almacen);
    }
}
