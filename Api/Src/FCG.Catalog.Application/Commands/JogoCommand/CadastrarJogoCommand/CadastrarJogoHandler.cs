using FCG.Catalog.Core.UnitOfWork;
using FCG.Catalog.Domain.Entities;
using MediatR;

namespace FCG.Catalog.Application.Commands.JogoCommand.CadastrarJogoCommand;

public class CadastrarJogoHandler : IRequestHandler<CadastrarJogoCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public CadastrarJogoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(CadastrarJogoCommand request, CancellationToken cancellationToken)
    {
        var jogoRepository = _unitOfWork.GetRepository<Jogo>();

        var jogoExistente = await jogoRepository.GetByAsync(predicate: j => j.Nome == request.Nome, cancellationToken: cancellationToken);
        if (jogoExistente is not null)
            throw new Exception("Jogo já existe.");

        var jogo = new Jogo(request.Nome, request.Preco, request.IdCategoria);

        await jogoRepository.AddAsync(jogo, cancellationToken);

        await _unitOfWork.SaveChanges();
    }
}
