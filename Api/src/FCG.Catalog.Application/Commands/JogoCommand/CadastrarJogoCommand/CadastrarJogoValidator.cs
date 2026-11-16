using FluentValidation;

namespace FCG.Catalog.Application.Commands.JogoCommand.CadastrarJogoCommand;

public class CadastrarJogoValidator : AbstractValidator<CadastrarJogoCommand>
{
    public CadastrarJogoValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do jogo é obrigatório.")
            .MinimumLength(3).WithMessage("O nome do jogo deve conter pelo menos 3 caracteres.");
    }
}
