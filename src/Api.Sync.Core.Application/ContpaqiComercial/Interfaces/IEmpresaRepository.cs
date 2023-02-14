using Api.Sync.Core.Application.ContpaqiComercial.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IEmpresaRepository
{
    Task<IEnumerable<EmpresaDto>> BuscarEmpresasAsync(CancellationToken cancellationToken);
}
