using ARSoftware.Contpaqi.Comercial.Sdk.Abstractions.Enums;

namespace Api.Sync.Core.Application.ContpaqiComercial.Interfaces;

public interface IDireccionRepository
{
    Direccion? BuscarDireccionPorId(int id);
    Direccion? BuscarDireccionPorCliente(string codigoCliente, TipoDireccion tipo);
}
