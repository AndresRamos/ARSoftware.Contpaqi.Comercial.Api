using ARSoftware.Contpaqi.Comercial.Sdk.Extras.Extensions;
using FluentValidation;

namespace Api.Core.Application.Validators;

public sealed class DatosExtraValidator<T> : AbstractValidator<KeyValuePair<string, string>>
{
    public DatosExtraValidator()
    {
        RuleFor(m => m)
            .Must(m => typeof(T).HasProperty(m.Key))
            .WithMessage(m => $"El dato [{m.Key}] no es un campo valido de la tabla {typeof(T).Name}.");
    }
}
