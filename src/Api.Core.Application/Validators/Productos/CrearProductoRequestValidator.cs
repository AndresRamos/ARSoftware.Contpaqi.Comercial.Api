using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Productos;

public sealed class CrearProductoRequestValidator : AbstractValidator<CrearProductoRequest>
{
    public CrearProductoRequestValidator()
    {
        RuleFor(c => c.Model.Producto.Tipo).NotEmpty();
        RuleFor(c => c.Model.Producto.Codigo).NotEmpty();
        RuleFor(c => c.Model.Producto.Nombre).NotEmpty();
        RuleFor(x => x.Model.Producto.DatosExtra).ForEach(rule => { rule.SetValidator(new DatosExtraValidator<admProductos>()); });
    }
}
