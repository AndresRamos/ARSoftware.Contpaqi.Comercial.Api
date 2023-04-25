using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Almacenes;

public sealed class ActualizarAlmacenRequestValidator : AbstractValidator<ActualizarAlmacenRequest>
{
    public ActualizarAlmacenRequestValidator()
    {
        RuleFor(x => x.Model.CodigoAlmacen).NotEmpty();

        RuleFor(x => x.Model.DatosAlmacen).NotEmpty().ForEach(rule => { rule.SetValidator(new DatosExtraValidator<admAlmacenes>()); });
    }
}
