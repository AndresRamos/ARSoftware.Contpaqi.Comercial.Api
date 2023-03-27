using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IProductoRepository
{
    Task<Producto?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<Producto?> BuscarPorCodigoAsync(string codigo,
                                         ILoadRelatedDataOptions loadRelatedDataOptions,
                                         CancellationToken cancellationToken);

    Task<IEnumerable<Producto>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<IEnumerable<Producto>> BuscarPorRequestModel(BuscarProductosRequestModel requestModel,
                                                      ILoadRelatedDataOptions loadRelatedDataOptions,
                                                      CancellationToken cancellationToken);
}
