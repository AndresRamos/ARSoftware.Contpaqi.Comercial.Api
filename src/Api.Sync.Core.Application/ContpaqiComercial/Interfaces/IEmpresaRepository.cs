using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IEmpresaRepository
{
    Task<IEnumerable<Empresa>> BuscarTodoAsync(ILoadRelatedDataOptions relatedDataOptions, CancellationToken cancellationToken);
}
