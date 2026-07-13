using FCG.Catalog.Application.Queries;
using FCG.Catalog.Application.Queries.DTOs;
using FCG.Catalog.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FCG.Catalog.Infra.Queries;

public class BibliotecaQueryService : IBibliotecaQueryService
{
    private readonly FcgCatalogDbContext _context;

    public BibliotecaQueryService(FcgCatalogDbContext context)
    {
        _context = context;
    }

    public async Task<BibliotecaDto> ObterBibliotecaUsuarioAsync(Guid id,CancellationToken cancellationToken)
    {
        var biblioteca = await _context.Bibliotecas
            .Include(x => x.JogosBiblioteca)
            .ThenInclude(y => y.Jogo)
            .FirstOrDefaultAsync(u => u.IdUsuario == id, cancellationToken);

        return new BibliotecaDto
        {
            Id = biblioteca.Id,
            Jogos = biblioteca.JogosBiblioteca
            .Select(j => new JogosDto
            {
                IdJogo = j.Jogo.Id,
                Nome = j.Jogo.Nome,
                DataAdicionadoEm = j.CreatedAt,

            }).ToList()
        };
    }
}
