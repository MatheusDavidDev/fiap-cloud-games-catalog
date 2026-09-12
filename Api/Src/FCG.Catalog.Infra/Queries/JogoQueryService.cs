using FCG.Catalog.Application.Cache;
using FCG.Catalog.Application.Queries;
using FCG.Catalog.Application.Queries.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FCG.Catalog.Infra.Queries;

public class JogoQueryService : IJogoQueryService
{
    private readonly FcgCatalogDbContext _context;
    private readonly ICacheService _cache;

    public JogoQueryService(FcgCatalogDbContext context, ICacheService cache)
    {
        _context = context;
        _cache = cache;
    }

    public async Task<JogoDto> ObterJogoPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.Jogo(id);

        var jogoCache = await _cache.GetAsync<JogoDto>(cacheKey, cancellationToken);

        if (jogoCache is not null)
        {
            return jogoCache;
        }

        var jogo = await _context.Jogos
            .Include(x => x.Categoria)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        var result = new JogoDto
        {
            Id = jogo.Id,
            Nome = jogo.Nome,
            PrecoOriginal = jogo.Preco,
            CategoriaNome = jogo.Categoria?.Nome ?? "Sem Categoria"
        };

        // Salva os dados da consulta no cache para futuras requisições
        await _cache.SetAsync(cacheKey, jogo, TimeSpan.FromMinutes(10),cancellationToken);

        return result;
    }

    public async Task<IEnumerable<JogoDto>> ObterJogosAsync(CancellationToken cancellationToken)
    {
        var cacheKey = CacheKeys.TodosJogos;

        var jogosCache = await _cache.GetAsync<List<JogoDto>>(cacheKey, cancellationToken);

        if (jogosCache is not null)
        {
            return jogosCache;
        }

        var jogos = await _context.Jogos
            .Include(x => x.Categoria)
            .ToListAsync(cancellationToken);

        var result = jogos
            .OrderByDescending(x => x.CreatedAt)
            .Select(x => new JogoDto
            {
                Id = x.Id,
                Nome = x.Nome,
                PrecoOriginal = x.Preco,
                CategoriaNome = x.Categoria?.Nome ?? "Sem Categoria"
            })
            .ToList();

        // Salva os dados da consulta no cache para futuras requisições
        await _cache.SetAsync(cacheKey, result, TimeSpan.FromMinutes(10), cancellationToken);

        return result;
    }
}
