using FCG.Catalog.Application.Cache;
using FCG.Catalog.Core.UnitOfWork;
using FCG.Catalog.Domain.Entities;
using MediatR;

namespace FCG.Catalog.Application.Commands.JogoCommand.CadastrarJogoCommand;

public class CadastrarJogoHandler : IRequestHandler<CadastrarJogoCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;

    public CadastrarJogoHandler(IUnitOfWork unitOfWork, ICacheService cache)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cache;
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

        await _cacheService.RemoveAsync(CacheKeys.TodosJogos, cancellationToken);
    }
}
