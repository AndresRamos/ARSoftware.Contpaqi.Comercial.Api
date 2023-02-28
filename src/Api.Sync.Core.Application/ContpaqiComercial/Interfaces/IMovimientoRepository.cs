using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IMovimientoRepository
{
    Task<IEnumerable<Movimiento>> BuscarPorDocumentoIdAsync(int documentoId, CancellationToken cancellationToken);
}
