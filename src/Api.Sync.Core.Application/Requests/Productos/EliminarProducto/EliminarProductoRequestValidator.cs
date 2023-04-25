using Api.Core.Domain.Requests;
using Api.Sync.Core.Application.ContpaqiComercial.Interfaces;
using FluentValidation;

namespace Api.Sync.Core.Application.Requests.Productos.EliminarProducto;

public sealed class EliminarProductoRequestValidator : AbstractValidator<EliminarProductoRequest>
{
    public EliminarProductoRequestValidator(IProductoRepository productoRepository)
    {
        RuleFor(m => m.Model.CodigoProducto)
            .MustAsync(async (s, token) => await productoRepository.ExistePorCodigoAsync(s, token))
            .WithMessage("El producto {PropertyValue} no existe.");
    }
}
