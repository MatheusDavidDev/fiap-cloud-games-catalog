using MediatR;

namespace FCG.Catalog.Application.Commands.JogoCommand.CadastrarJogoCommand;

public class CadastrarJogoCommand : IRequest
{
    public CadastrarJogoCommand(string nome, decimal preco, Guid? idCategoria)
    {
        Nome = nome;
        Preco = preco;
        IdCategoria = idCategoria;
    }

    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public Guid? IdCategoria { get; set; }
}
