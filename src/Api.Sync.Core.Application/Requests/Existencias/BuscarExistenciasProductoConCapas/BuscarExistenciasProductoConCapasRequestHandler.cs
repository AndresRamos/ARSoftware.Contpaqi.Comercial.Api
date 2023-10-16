using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using MediatR;

namespace Api.Sync.Core.Application.Requests.Existencias.BuscarExistenciasProductoConCapas;

public sealed class
    BuscarExistenciasProductoConCapasRequestHandler : IRequestHandler<BuscarExistenciasProductoConCapasRequest,
        BuscarExistenciasProductoConCapasResponse>
{
    private readonly IExistenciasProductoRepository _existenciasProductoRepository;

    public BuscarExistenciasProductoConCapasRequestHandler(IExistenciasProductoRepository existenciasProductoRepository)
    {
        _existenciasProductoRepository = existenciasProductoRepository;
    }

    public async Task<BuscarExistenciasProductoConCapasResponse> Handle(BuscarExistenciasProductoConCapasRequest request,
        CancellationToken cancellationToken)
    {
        double exitencias = await _existenciasProductoRepository.BuscaExistenciasConCapasAsync(request.Model, cancellationToken);

        return BuscarExistenciasProductoConCapasResponse.CreateInstance(exitencias);
    }
}