﻿using Api.Core.Domain.Common;
using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IFolioDigitalRepository
{
    Task<FolioDigital?> BuscarPorDocumentoIdAsync(int conceptoId, int documentoId, ILoadRelatedDataOptions loadRelatedDataOptions,
        CancellationToken cancellationToken);
}
