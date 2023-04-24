using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Agentes;

public sealed class ActualizarAgenteRequestValidator : AbstractValidator<ActualizarAgenteRequest>
{
    public ActualizarAgenteRequestValidator()
    {
        RuleFor(x => x.Model.CodigoAgente).NotEmpty();

        RuleFor(x => x.Model.DatosAgente).NotEmpty().ForEach(rule => { rule.SetValidator(new DatosExtraValidator<admAgentes>()); });
    }
}
