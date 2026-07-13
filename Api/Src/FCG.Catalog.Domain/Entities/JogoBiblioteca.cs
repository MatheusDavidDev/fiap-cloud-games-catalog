using FCG.Catalog.Core.Models;

namespace FCG.Catalog.Domain.Entities;

public class JogoBiblioteca : BaseEntity
{
    public JogoBiblioteca(Guid idJogo, Guid idBiblioteca)
    {
        IdJogo = idJogo;
        IdBiblioteca = idBiblioteca;
    }

    public Guid IdBiblioteca { get; private set; }
    public Biblioteca Biblioteca { get; private set; }
    public Guid IdJogo { get; private set; }
    public Jogo Jogo { get; private set; }
}
