using FluentValidation;
using Mongo.Domain.Schemas;

namespace Mongo.Domain.Validations
{
    public class AvaliacaoValidator : AbstractValidator<AvaliacaoSchema>
    {
        public AvaliacaoValidator()
        {
            RuleFor(a => a.Estrelas)
                .GreaterThan(0).WithMessage("Número de estrelas deve ser maior que zero.")
                .LessThanOrEqualTo(5).WithMessage("Número de estrelas deve ser menor ou igual a cinco.");

            RuleFor(a => a.Comentario)
                .NotEmpty().WithMessage("Comentário é campo obrigatório.")
                .MaximumLength(100).WithMessage("Comentário pode ter no máximo 100 caracteres.");
        }
    }
}
