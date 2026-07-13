using FCG.Catalog.Core.Models;

namespace FCG.Catalog.Domain.Entities;

public class Categoria : BaseEntity
{
    public Categoria(string nome)
    {
        Nome = nome;
    }

    public string Nome { get; private set; }
    public ICollection<Jogo> Jogos { get; private set; }

    public void Atualizar(string nome, string descricao)
    {
        Nome = nome;
        SetUpdated();
    }
}
