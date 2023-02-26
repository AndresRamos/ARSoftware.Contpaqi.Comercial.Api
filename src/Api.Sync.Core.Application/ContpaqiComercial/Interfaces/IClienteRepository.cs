using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IClienteRepository
{
    Task<bool> ExistePorCodigoAsync(string codigo, CancellationToken cancellationToken);
    Task<Cliente?> BuscarPorCodigoAsync(string codigo, CancellationToken cancellationToken);
    Task<Cliente?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<Cliente>> BuscarTodoAsync(CancellationToken cancellationToken);
    Task<bool> ExisteDireccionFiscalDelClienteAsync(string codigo, CancellationToken cancellationToken);
}
