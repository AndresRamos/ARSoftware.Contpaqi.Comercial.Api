using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IClienteRepository
{
    Task<ClienteProveedor?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<ClienteProveedor?> BuscarPorCodigoAsync(string codigo, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken);

    Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken);

    Task<IEnumerable<ClienteProveedor>> BuscarPorRequestModel(BuscarClientesRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<bool> ExisteDireccionFiscalDelClienteAsync(string codigo, CancellationToken cancellationToken);
}
