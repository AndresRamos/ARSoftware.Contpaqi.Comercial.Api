using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IConceptoRepository
{
    Task<Concepto?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    Task<Concepto?> BuscarPorCodigoAsync(string codigo, CancellationToken cancellationToken);
    Task<IEnumerable<Concepto>> BuscarTodoAsync(CancellationToken cancellationToken);
}
