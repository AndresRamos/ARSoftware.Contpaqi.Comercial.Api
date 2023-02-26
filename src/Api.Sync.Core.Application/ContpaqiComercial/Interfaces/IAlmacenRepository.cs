using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IAlmacenRepository
{
    Task<Almacen?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    Task<Almacen?> BuscarPorCodigoAsync(string codigo, CancellationToken cancellationToken);
    Task<IEnumerable<Almacen>> BuscarTodoAsync(CancellationToken cancellationToken);
}
