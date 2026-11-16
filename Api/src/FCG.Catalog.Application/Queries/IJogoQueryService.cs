using FCG.Catalog.Application.Queries.DTOs;

namespace FCG.Catalog.Application.Queries;

public interface IJogoQueryService
{
    Task<JogoDto> ObterJogoPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<JogoDto>> ObterJogosAsync(CancellationToken cancellationToken);
}
