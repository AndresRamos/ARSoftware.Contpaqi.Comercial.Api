﻿using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IConceptoRepository
{
    Task<Concepto?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<Concepto?> BuscarPorCodigoAsync(string codigo,
                                         ILoadRelatedDataOptions loadRelatedDataOptions,
                                         CancellationToken cancellationToken);

    Task<IEnumerable<Concepto>> BuscarTodoAsync(ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);
}
