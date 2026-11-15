using FCG.Catalog.Application.Interfaces.Models;

namespace FCG.Catalog.Application.Interfaces;

public interface IJogoDetalhesRepository
{
    Task SalvarAsync(Guid jogoId, string? descricao, string? desenvolvedora, List<string>? generos, 
        List<string>? plataformas, string? requisitosMinimos,CancellationToken cancellationToken = default);

    Task<JogoDetalhesModel?> ObterPorJogoIdAsync(Guid jogoId, CancellationToken cancellationToken = default);
}
