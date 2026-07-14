using FCG.Catalog.Core.UnitOfWork;
using FCG.Catalog.Domain.Entities;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FCG.Catalog.Application.Commands.BibliotecaCommand.AdicionarJogoCommand;

public class AdicionarJogoHandler : IRequestHandler<AdicionarJogoCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AdicionarJogoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

    }

    public async Task Handle(AdicionarJogoCommand request, CancellationToken cancellationToken)
    {
        var bibliotecaRepository = _unitOfWork.GetRepository<Biblioteca>();
        var jogoRepository = _unitOfWork.GetRepository<Jogo>();
        var jogoBibliotecaRepository = _unitOfWork.GetRepository<JogoBiblioteca>();

        var biblioteca = await bibliotecaRepository.GetByAsync(
            predicate: x => x.IdUsuario == request.IdUsuario, 
            include: x => x.Include(i => i.JogosBiblioteca),cancellationToken: cancellationToken);

        var jogo = await jogoRepository.GetByAsync(predicate: x => x.Id == request.IdJogo, cancellationToken: cancellationToken);

        if (biblioteca.JogosBiblioteca.Any(x => x.IdJogo == request.IdJogo))
            throw new Exception("Jogo já adicionado à biblioteca.");

        if (jogo is null)
            throw new Exception("Jogo não existe.");

        var jogoBiblioteca = new JogoBiblioteca(jogo.Id, biblioteca.Id);

        await jogoBibliotecaRepository.AddAsync(jogoBiblioteca, cancellationToken);

        await _unitOfWork.SaveChanges();

    }
}

