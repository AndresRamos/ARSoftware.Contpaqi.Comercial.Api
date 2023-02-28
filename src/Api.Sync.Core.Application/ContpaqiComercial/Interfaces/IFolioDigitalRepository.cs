using Api.Core.Domain.Models;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IFolioDigitalRepository
{
    Task<FolioDigital?> BuscarPorDocumentoIdAsync(int conceptoId, int documentoId, CancellationToken cancellationToken);
}
