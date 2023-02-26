using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IProductoRepository
{
    Task<Producto?> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    Task<Producto?> BuscarPorCodigoAsync(string codigo, CancellationToken cancellationToken);
    Task<IEnumerable<Producto>> BuscarTodoAsync(CancellationToken cancellationToken);
}
