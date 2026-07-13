using FCG.Catalog.Core.Models;

namespace FCG.Catalog.Domain.Entities;

public class Jogo : BaseEntity
{
    public Jogo(string nome, decimal preco, Guid? idCategoria)
    {
        Nome = nome;
        Preco = preco;
        IdCategoria = idCategoria;
    }

    public string Nome { get; private set; }
    public decimal Preco { get; private set; }
    public Guid? IdCategoria { get; private set; }
    public Categoria Categoria { get; private set; }
    public ICollection<JogoBiblioteca> JogoBibliotecas { get; private set; }

}
