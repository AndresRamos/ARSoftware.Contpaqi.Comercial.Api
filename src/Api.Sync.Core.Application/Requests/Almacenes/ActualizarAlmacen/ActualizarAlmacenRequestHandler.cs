using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Almacenes.ActualizarAlmacen;

public sealed class ActualizarAlmacenRequestHandler : IRequestHandler<ActualizarAlmacenRequest, ActualizarAlmacenResponse>
{
    private readonly IAlmacenRepository _almacenRepository;
    private readonly IAlmacenService _almacenService;

    public ActualizarAlmacenRequestHandler(IAlmacenService almacenService, IAlmacenRepository almacenRepository)
    {
        _almacenService = almacenService;
        _almacenRepository = almacenRepository;
    }

    public async Task<ActualizarAlmacenResponse> Handle(ActualizarAlmacenRequest request, CancellationToken cancellationToken)
    {
        _almacenService.Actualizar(request.Model.CodigoAlmacen, request.Model.DatosAlmacen);

        Almacen almacen = await _almacenRepository.BuscarPorCodigoAsync(request.Model.CodigoAlmacen, request.Options, cancellationToken) ??
                          throw new InvalidOperationException();

        return ActualizarAlmacenResponse.CreateInstance(almacen);
    }
}
