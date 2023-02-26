using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IAgenteRepository
{
    Task<Agente?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    Task<Agente?> BuscarPorCodigoAsync(string codigo, CancellationToken cancellationToken);
    Task<IEnumerable<Agente>> BuscarTodoAsync(CancellationToken cancellationToken);
}
