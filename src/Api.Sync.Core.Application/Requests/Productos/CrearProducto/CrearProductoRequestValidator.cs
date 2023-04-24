using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Productos.CrearProducto;

public sealed class CrearProductoRequestValidator : AbstractValidator<CrearProductoRequest>
{
    public CrearProductoRequestValidator(IProductoRepository productoRepository)
    {
        RuleFor(m => m.Model.Producto.Codigo)
            .MustAsync(async (s, token) => await productoRepository.ExistePorCodigoAsync(s, token) == false)
            .WithMessage("El producto {PropertyValue} ya existe.");
    }
}
