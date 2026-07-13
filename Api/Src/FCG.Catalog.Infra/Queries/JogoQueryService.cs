using FCG.Catalog.Application.Queries;
using FCG.Catalog.Application.Queries.DTOs;
using Microsoft.EntityFrameworkCore;

namespace FCG.Catalog.Infra.Queries;

public class JogoQueryService : IJogoQueryService
{
    private readonly FcgCatalogDbContext _context;

    public JogoQueryService(FcgCatalogDbContext context)
    {
        _context = context;
    }

    public async Task<JogoDto> ObterJogoPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var jogo = await _context.Jogos
            .Include(x => x.Categoria)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return new JogoDto
        {
            Id = jogo.Id,
            Nome = jogo.Nome,
            PrecoOriginal = jogo.Preco,
            CategoriaNome = jogo.Categoria.Nome
        };
    }

    public async Task<IEnumerable<JogoDto>> ObterJogosAsync(CancellationToken cancellationToken)
    {
        var jogos = await _context.Jogos
            .Include(x => x.Categoria)
            .ToListAsync(cancellationToken);

        return jogos.OrderByDescending(x => x.CreatedAt)
            .Select(x =>
            {
                return new JogoDto
                {
                    Id = x.Id,
                    Nome = x.Nome,
                    PrecoOriginal = x.Preco,
                    CategoriaNome = x.Categoria?.Nome ?? "Sem Categoria"
                };
            });
    }
}
