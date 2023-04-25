using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Productos;

public sealed class ActualizarProductoRequestValidator : AbstractValidator<ActualizarProductoRequest>
{
    public ActualizarProductoRequestValidator()
    {
        RuleFor(c => c.Model.CodigoProducto).NotEmpty();
        RuleFor(c => c.Model.DatosProducto).NotEmpty().ForEach(rule => rule.SetValidator(new DatosExtraValidator<admProductos>()));
    }
}
