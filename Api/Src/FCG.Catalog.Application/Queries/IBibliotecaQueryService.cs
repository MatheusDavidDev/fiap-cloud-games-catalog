using FCG.Catalog.Application.Queries.DTOs;

namespace FCG.Catalog.Application.Queries;

public interface IBibliotecaQueryService
{
    Task<BibliotecaDto> ObterBibliotecaUsuarioAsync(Guid id, CancellationToken cancellationToken);
}
