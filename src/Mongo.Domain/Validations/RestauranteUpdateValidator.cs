using FluentValidation;
using Mongo.Domain.Dtos.Restaurante;

namespace Mongo.Domain.Validations
{
    public class RestauranteUpdateValidator : AbstractValidator<RestauranteDtoUpdate>
    {
        public RestauranteUpdateValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("Nome - é campo obrigatório")
                .MaximumLength(100).WithMessage("Nome - deve ter máximo de 100 caracteres");

            RuleFor(x => x.Cozinha)
                .NotEmpty().WithMessage("Cozinha - é campo obrigatório")
                .IsInEnum().WithMessage("Cozinha - Opção inválida");

            RuleFor(x => x.Logradouro)
                .NotEmpty().WithMessage("Logradouro - é campo obrigatório")
                .MaximumLength(100).WithMessage("Logradouro - deve ter máximo de 100 caracteres");

            RuleFor(x => x.Cidade)
                .NotEmpty().WithMessage("Cidade - é campo obrigatório")
                .MaximumLength(50).WithMessage("Cidade - deve ter máximo de 50 caracteres");

            RuleFor(x => x.Uf)
                .NotEmpty().WithMessage("Uf - é campo obrigatório")
                .MaximumLength(2).WithMessage("Uf - deve ter 2 caracteres");

            RuleFor(x => x.Cep)
                .NotEmpty().WithMessage("Cep - é campo obrigatório")
                .Length(8).WithMessage("Cep - deve ter 8 caracteres");
        }
    }
}
