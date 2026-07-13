using MediatR;

namespace FCG.Catalog.Application.Commands.BibliotecaCommand.AdicionarJogoCommand;

public class AdicionarJogoCommand : IRequest
{
    public AdicionarJogoCommand(Guid idOrdemCompra, Guid idUsuario, Guid idJogo)
    {
        IdOrdemCompra = idOrdemCompra;
        IdUsuario = idUsuario;
        IdJogo = idJogo;
    }

    public Guid IdOrdemCompra { get; set; }
    public Guid IdUsuario { get; set; }
    public Guid IdJogo { get; set; }
}
