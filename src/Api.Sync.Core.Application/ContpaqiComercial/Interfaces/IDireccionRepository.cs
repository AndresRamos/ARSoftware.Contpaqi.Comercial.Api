namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IDireccionRepository
{
    Direccion? BuscarDireccionPorId(int id);
}
