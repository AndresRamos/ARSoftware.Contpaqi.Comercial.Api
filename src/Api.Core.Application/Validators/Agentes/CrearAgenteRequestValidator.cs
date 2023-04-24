using Api.Core.Domain.Requests;
using ARSoftware.Contpaqi.Comercial.Sql.Models.Empresa;
using FluentValidation;

namespace Api.Core.Application.Validators.Agentes;

public sealed class CrearAgenteRequestValidator : AbstractValidator<CrearAgenteRequest>
{
    public CrearAgenteRequestValidator()
    {
        RuleFor(c => c.Model.Agente.Codigo).NotEmpty().WithMessage("El código del agente es requerido.");

        RuleFor(c => c.Model.Agente.Nombre).NotEmpty().WithMessage("El nombre del agente es requerido.");

        RuleFor(c => c.Model.Agente.Tipo).NotEmpty().WithMessage("El tipo del agente es requerido.");

        RuleFor(x => x.Model.Agente.DatosExtra).ForEach(rule => { rule.SetValidator(new DatosExtraValidator<admAgentes>()); });
    }
}
