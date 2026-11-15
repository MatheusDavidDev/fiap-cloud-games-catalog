using FCG.Catalog.Application.Cache;
using FCG.Catalog.Application.Interfaces;
using FCG.Catalog.Core.UnitOfWork;
using FCG.Catalog.Domain.Entities;
using MediatR;

namespace FCG.Catalog.Application.Commands.JogoCommand.AtualizarJogoCommand;

public class AtualizarJogoHandler : IRequestHandler<AtualizarJogoCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICacheService _cacheService;
    private readonly IJogoDetalhesRepository _jogoDetalhesRepository;
    private readonly IJogoSearchService _jogoSearchService;

    public AtualizarJogoHandler(IUnitOfWork unitOfWork, ICacheService cacheService, 
        IJogoDetalhesRepository jogoDetalhesRepository,IJogoSearchService jogoSearchService)
    {
        _unitOfWork = unitOfWork;
        _cacheService = cacheService;
        _jogoDetalhesRepository = jogoDetalhesRepository;
        _jogoSearchService = jogoSearchService;
    }

    public async Task Handle(AtualizarJogoCommand request, CancellationToken cancellationToken)
    {
        var jogoRepository = _unitOfWork.GetRepository<Jogo>();

        // Buscar jogo no SQL Server
        var jogo = await jogoRepository.GetByAsync(
            predicate: x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (jogo is null)
            throw new Exception("Jogo não encontrado.");

        // Atualizar SQL Server
        jogo.Atualizar(request.Nome, request.Preco, request.IdCategoria);

        await _unitOfWork.SaveChanges();

        // Invalidar cache Redis
        await _cacheService.RemoveAsync(
            CacheKeys.TodosJogos,
            cancellationToken);

        // Atualizar MongoDB
        await _jogoDetalhesRepository.SalvarAsync(
            jogo.Id,
            request.Descricao,
            request.Desenvolvedora,
            request.Generos,
            request.Plataformas,
            request.RequisitosMinimos,
            cancellationToken);

        // Atualizar OpenSearch
        await _jogoSearchService.IndexarJogoAsync(
            jogo.Id,
            jogo.Nome,
            jogo.Preco,
            jogo.IdCategoria,
            request.Descricao,
            request.Desenvolvedora,
            request.Generos,
            request.Plataformas,
            cancellationToken);
    }
}