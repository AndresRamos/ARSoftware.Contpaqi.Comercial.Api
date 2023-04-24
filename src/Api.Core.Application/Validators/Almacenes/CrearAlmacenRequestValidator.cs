using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Almacenes;

public sealed class CrearAlmacenRequestValidator : AbstractValidator<CrearAlmacenRequest>
{
    public CrearAlmacenRequestValidator()
    {
        RuleFor(c => c.Model.Almacen.Codigo).NotEmpty();

        RuleFor(c => c.Model.Almacen.Nombre).NotEmpty();

        RuleFor(x => x.Model.Almacen.DatosExtra).ForEach(rule => { rule.SetValidator(new DatosExtraValidator<admAlmacenes>()); });
    }
}
