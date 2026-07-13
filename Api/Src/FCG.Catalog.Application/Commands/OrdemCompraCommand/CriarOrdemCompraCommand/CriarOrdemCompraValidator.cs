using FluentValidation;

namespace FCG.Catalog.Application.Commands.OrdemCompraCommand.CriarOrdemCompraCommand;

public class CriarOrdemCompraValidator : AbstractValidator<CriarOrdemCompraCommand>
{
    public CriarOrdemCompraValidator()
    {
        RuleFor(x => x.IdUsuario)
            .NotEmpty()
            .WithMessage("O Id do usuário é obrigatório.");

        RuleFor(x => x.IdJogo)
            .NotEmpty()
            .WithMessage("O Id do jogo é obrigatório.");
    }
}
