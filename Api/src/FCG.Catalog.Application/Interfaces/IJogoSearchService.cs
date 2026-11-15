using FCG.Catalog.Application.Interfaces.Models;

namespace FCG.Catalog.Application.Interfaces;

public interface IJogoSearchService
{
    Task IndexarJogoAsync(Guid id, string nome, decimal preco, Guid? idCategoria, string? descricao,
        string? desenvolvedora, List<string>? generos, List<string>? plataformas, 
        CancellationToken cancellationToken = default);

    Task<List<JogoSearchResultModel>> BuscarJogosAsync(string pesquisa, CancellationToken cancellationToken = default);
}
