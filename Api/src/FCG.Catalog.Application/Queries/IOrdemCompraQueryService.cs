using FCG.Catalog.Application.Queries.DTOs;

namespace FCG.Catalog.Application.Queries;

public interface IOrdemCompraQueryService
{
    Task<OrdemCompraDto> ObterOrdemPorIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<OrdemCompraDto>> ObterOrdemUsuarioAsync(Guid idUsuario, CancellationToken cancellationToken);
}
