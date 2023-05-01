using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Almacenes.BuscarAlmacenes;

public sealed class BuscarAlmacenesRequestHandler : IRequestHandler<BuscarAlmacenesRequest, BuscarAlmacenesResponse>
{
    private readonly IAlmacenRepository _almacenRepository;

    public BuscarAlmacenesRequestHandler(IAlmacenRepository almacenRepository)
    {
        _almacenRepository = almacenRepository;
    }

    public async Task<BuscarAlmacenesResponse> Handle(BuscarAlmacenesRequest request, CancellationToken cancellationToken)
    {
        List<Almacen> almacenes = (await _almacenRepository.BuscarPorRequestModelAsync(request.Model, request.Options, cancellationToken))
            .ToList();

        return BuscarAlmacenesResponse.CreateInstance(almacenes);
    }
}
