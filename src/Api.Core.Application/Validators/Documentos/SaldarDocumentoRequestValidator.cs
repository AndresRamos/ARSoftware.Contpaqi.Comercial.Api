using Api.Core.Domain.Requests;
using FluentValidation;

namespace Api.Core.Application.Validators.Documentos;

public sealed class SaldarDocumentoRequestValidator : AbstractValidator<SaldarDocumentoRequest>
{
    public SaldarDocumentoRequestValidator()
    {
        RuleFor(m => m.Model.DocumentoAPagar.ConceptoCodigo).NotEmpty();
        RuleFor(m => m.Model.DocumentoAPagar.Folio).NotEmpty();

        RuleFor(m => m.Model.DocumentoPago.ConceptoCodigo).NotEmpty();
        RuleFor(m => m.Model.DocumentoPago.Folio).NotEmpty();
    }
}
