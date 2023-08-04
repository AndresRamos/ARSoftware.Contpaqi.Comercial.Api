using Api.Core.Domain.Common;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IEmpresaRepository
{
    Task<IEnumerable<Empresa>> BuscarTodoAsync(ILoadRelatedDataOptions relatedDataOptions, CancellationToken cancellationToken);
}
