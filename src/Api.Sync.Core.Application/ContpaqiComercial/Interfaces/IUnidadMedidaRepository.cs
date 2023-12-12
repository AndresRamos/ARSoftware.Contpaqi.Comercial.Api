namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IUnidadMedidaRepository
{
    UnidadMedida? BuscarPorId(int id);

    UnidadMedida? BuscarPorNombre(string nombre);
}
