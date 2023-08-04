using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IAlmacenRepository
{
    Task<Almacen?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
    Task<Almacen?> BuscarPorCodigoAsync(string codigo, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
    Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken);
    Task<IEnumerable<Almacen>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<IEnumerable<Almacen>> BuscarPorRequestModelAsync(BuscarAlmacenesRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
