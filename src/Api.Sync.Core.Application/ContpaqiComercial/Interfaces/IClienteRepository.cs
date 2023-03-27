using Api.Core.Domain.Common;
using Api.Core.Domain.Models;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> BuscarPorCodigoAsync(string codigo, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
    Task<Cliente?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
    Task<IEnumerable<Cliente>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<IEnumerable<Cliente>> BuscarPorRequestModel(BuscarClientesRequestModel requestModel,
                                                     ILoadRelatedDataOptions loadRelatedDataOptions,
                                                     CancellationToken cancellationToken);

    Task<bool> ExisteDireccionFiscalDelClienteAsync(string codigo, CancellationToken cancellationToken);
}
