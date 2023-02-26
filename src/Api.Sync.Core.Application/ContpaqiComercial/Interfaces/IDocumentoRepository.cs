using Api.Core.Domain.Models;
using ARSoftware.Contpaqi.Comercial.Sdk.DatosAbstractos;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IDocumentoRepository
{
    Task<Documento> BuscarPorIdAsync(int id, CancellationToken cancellationToken);
    Task<tLlaveDoc> BuscarLlavePorIdAsync(int id, CancellationToken cancellationToken);
    Task<Documento> BuscarPorLlaveAsync(LlaveDocumento llaveDocumento, CancellationToken cancellationToken);
    Task<int> BusarIdPorLlaveAsync(LlaveDocumento llaveDocumento, CancellationToken cancellationToken);
}
