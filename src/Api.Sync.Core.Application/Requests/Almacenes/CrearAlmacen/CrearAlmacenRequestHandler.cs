using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
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
        int almacenId = _almacenService.Crear(request.Model.Almacen);

        Almacen almacen = await _almacenRepository.BuscarPorIdAsync(almacenId, request.Options, cancellationToken) ??
                          throw new InvalidOperationException();

        return CrearAlmacenResponse.CreateInstance(almacen);
    }
}
