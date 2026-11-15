using FluentValidation;

namespace FCG.Catalog.Application.Commands.JogoCommand.AtualizarJogoCommand;

public class AtualizarJogoValidator : AbstractValidator<AtualizarJogoCommand>
{
    public AtualizarJogoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("O ID do jogo é obrigatório.");

        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do jogo é obrigatório.")
            .MinimumLength(3).WithMessage("O nome do jogo deve conter pelo menos 3 caracteres.");
    }
}
