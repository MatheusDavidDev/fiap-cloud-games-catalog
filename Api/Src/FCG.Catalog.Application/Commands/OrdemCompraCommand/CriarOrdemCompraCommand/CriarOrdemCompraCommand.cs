using MediatR;

namespace FCG.Catalog.Application.Commands.OrdemCompraCommand.CriarOrdemCompraCommand;

public class CriarOrdemCompraCommand : IRequest
{
    public CriarOrdemCompraCommand(Guid idUsuario, Guid idJogo)
    {
        IdUsuario = idUsuario;
        IdJogo = idJogo;
    }

    public Guid IdUsuario { get; private set; }
    public Guid IdJogo { get; private set; }
}
