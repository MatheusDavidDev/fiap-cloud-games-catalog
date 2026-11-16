using FCG.Catalog.Application.Queries;
using FCG.Catalog.Application.Queries.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.Arm;

namespace FCG.Catalog.Infra.Queries;

public class OrdemCompraQueryService : IOrdemCompraQueryService
{
    private readonly FcgCatalogDbContext _context;

    public OrdemCompraQueryService(FcgCatalogDbContext context)
    {
        _context = context;
    }

    public async Task<OrdemCompraDto> ObterOrdemPorIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var ordem = await _context.OrdensCompra
            .Include(x => x.Jogo)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        return new OrdemCompraDto
        {
            Id = ordem.Id,
            NomeJogo = ordem.Jogo.Nome,
            Valor = ordem.Valor,
            Status = ordem.Status.ToString(),
            CreatedAt = ordem.CreatedAt,
            UpdatedAt = ordem.UpdatedAt
        };
    }

    public async Task<IEnumerable<OrdemCompraDto>> ObterOrdemUsuarioAsync(Guid idUsuario, CancellationToken cancellationToken)
    {
        var ordens = await _context.OrdensCompra
            .Where(x => x.IdUsuario == idUsuario)
            .Include(x => x.Jogo)
            .ToListAsync(cancellationToken);

        return ordens.OrderByDescending(x => x.CreatedAt)
            .Select(x =>
            {
                return new OrdemCompraDto
                {
                    Id = x.Id,
                    NomeJogo = x.Jogo.Nome,
                    Valor = x.Valor,
                    Status = x.Status.ToString(),
                    CreatedAt = x.CreatedAt,
                    UpdatedAt = x.UpdatedAt
                };
            });
    }

}
