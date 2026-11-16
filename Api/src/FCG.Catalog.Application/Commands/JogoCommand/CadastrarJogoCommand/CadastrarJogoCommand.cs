using MediatR;

namespace FCG.Catalog.Application.Commands.JogoCommand.CadastrarJogoCommand;

public class CadastrarJogoCommand : IRequest
{
    public CadastrarJogoCommand(string nome, decimal preco, Guid? idCategoria, string? descricao = null, 
        string? desenvolvedora = null, List<string>? generos = null, List<string>? plataformas = null, string? requisitosMinimos = null)
    {
        Nome = nome;
        Preco = preco;
        IdCategoria = idCategoria;
        Descricao = descricao;
        Desenvolvedora = desenvolvedora;
        Generos = generos;
        Plataformas = plataformas;
        RequisitosMinimos = requisitosMinimos;

    }

    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public Guid? IdCategoria { get; set; }

    // Dados do MongoDB
    public string? Descricao { get; set; }
    public string? Desenvolvedora { get; set; }
    public List<string>? Generos { get; set; }
    public List<string>? Plataformas { get; set; }
    public string? RequisitosMinimos { get; set; }
}
