using FCG.Catalog.Core.Models;

namespace FCG.Catalog.Domain.Entities;

public class Biblioteca : BaseEntity
{
    public Biblioteca(Guid idUsuario)
    {
        IdUsuario = idUsuario;
        JogosBiblioteca = new List<JogoBiblioteca>();
    }

    public Guid IdUsuario { get; private set; }
    public ICollection<JogoBiblioteca> JogosBiblioteca { get; private set; }

    //public void AddGame(Guid idJogo)
    //{
    //    if (JogoBiblioteca.Any(x => x.IdJogo == idJogo))
    //        throw new DomainException("O jogo já pertence à biblioteca.");

    //    JogoBiblioteca(new JogoBiblioteca(idJogo, this.Id));
    //}
}
