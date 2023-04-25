using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Productos.ActualizarProducto;

public sealed class ActualizarProductoRequestValidator : AbstractValidator<ActualizarProductoRequest>
{
    public ActualizarProductoRequestValidator(IProductoRepository productoRepository)
    {
        RuleFor(m => m.Model.CodigoProducto)
            .MustAsync(async (s, token) => await productoRepository.ExistePorCodigoAsync(s, token))
            .WithMessage("El producto {PropertyValue} no existe.");
    }
}
