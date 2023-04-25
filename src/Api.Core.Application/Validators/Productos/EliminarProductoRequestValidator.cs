using Api.Core.Domain.Requests;
using FluentValidation;

namespace Api.Core.Application.Validators.Productos;

public sealed class EliminarProductoRequestValidator : AbstractValidator<EliminarProductoRequest>
{
    public EliminarProductoRequestValidator()
    {
        RuleFor(c => c.Model.CodigoProducto).NotEmpty();
    }
}
