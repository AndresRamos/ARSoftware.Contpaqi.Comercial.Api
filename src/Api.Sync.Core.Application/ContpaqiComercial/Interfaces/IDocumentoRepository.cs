using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IDocumentoRepository
{
    Task<Documento> BuscarDocumentoPorIdAsync(int id, CancellationToken cancellationToken);
    Task<tLlaveDoc> BuscarLlavePorIdAsync(int id, CancellationToken cancellationToken);
}
