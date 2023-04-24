using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Agentes;

public sealed class ActualizarAgenteRequestValidator : AbstractValidator<ActualizarAgenteRequest>
{
    public ActualizarAgenteRequestValidator()
    {
        RuleFor(x => x.Model.CodigoAgente).NotEmpty().WithMessage("El código del agente es requerido.");

        RuleFor(x => x.Model.DatosAgente)
            .NotEmpty()
            .WithMessage("Los datos del agente son requeridos.")
            .ForEach(rule => { rule.SetValidator(new DatosExtraValidator<admAgentes>()); });
    }
}
