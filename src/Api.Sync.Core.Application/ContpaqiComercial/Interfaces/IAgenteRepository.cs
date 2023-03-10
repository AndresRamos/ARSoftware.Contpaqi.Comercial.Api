using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IAgenteRepository
{
    Task<Agente?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
    Task<Agente?> BuscarPorCodigoAsync(string codigo, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
    Task<IEnumerable<Agente>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
