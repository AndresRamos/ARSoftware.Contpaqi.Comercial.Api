﻿using Api.Core.Domain.Common;
using Api.Core.Domain.Requests;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IDocumentoRepository
{
    Task<Documento?> BuscarPorIdAsync(int id, ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<Documento?> BuscarPorLlaveAsync(LlaveDocumento llaveDocumento, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken);

    Task<bool> ExistePorLlaveAsync(LlaveDocumento llaveDocumento, CancellationToken cancellationToken);

    Task<IEnumerable<Documento>> BuscarPorRequestModelAsync(BuscarDocumentosRequestModel requestModel,
        ILoadRelatedDataOptions loadRelatedDataOptions, CancellationToken cancellationToken);

    Task<int> BusarIdPorLlaveAsync(LlaveDocumento llaveDocumento, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken);
}
