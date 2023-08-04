using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IConceptoRepository
{
    Task<ConceptoDocumento?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<ConceptoDocumento?> BuscarPorCodigoAsync(string codigo, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken);

    Task<IEnumerable<ConceptoDocumento>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken);

    Task<IEnumerable<ConceptoDocumento>> BuscarPorRequstModelAsync(BuscarConceptosRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
