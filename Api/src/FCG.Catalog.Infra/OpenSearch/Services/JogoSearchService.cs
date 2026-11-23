using FCG.Catalog.Application.Interfaces;
using FCG.Catalog.Application.Interfaces.Models;
using FCG.Catalog.Infra.OpenSearch.Models;
using OpenSearch.Client;
using OpenSearch.Net;

namespace FCG.Catalog.Infra.OpenSearch.Services;

public class JogoSearchService : IJogoSearchService
{
    private readonly IOpenSearchClient _client;

    public JogoSearchService(IOpenSearchClient client)
    {
        _client = client;
    }

    public async Task IndexarJogoAsync(Guid id, string nome, decimal preco, Guid? idCategoria, 
        string? descricao, string? desenvolvedora, List<string>? generos, List<string>? plataformas,
        CancellationToken cancellationToken = default)
    {
        var documento = new JogoIndexDocument
        {
            Id = id,
            Nome = nome,
            Preco = preco,
            IdCategoria = idCategoria,
            Descricao = descricao,
            Desenvolvedora = desenvolvedora,
            Generos = generos ?? [],
            Plataformas = plataformas ?? []
        };

        var response = await _client.IndexAsync(
            documento,
            i => i
                .Index("jogos")
                .Id(id.ToString())
                .Refresh(Refresh.WaitFor),
            cancellationToken);

        if (!response.IsValid)
        {
            throw new Exception(
                $"Erro ao indexar jogo no OpenSearch: {response.DebugInformation}");
        }
    }

    public async Task<List<JogoSearchResultModel>> BuscarJogosAsync(string pesquisa, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(pesquisa))
            return [];

        var response = await _client.SearchAsync<JogoIndexDocument>(
            s => s
                .Index("jogos")
                .Size(20)
                .Query(x => x
                    .Bool(x => x
                        .Should(
                            s => s.Match(m => m
                                .Field(x => x.Nome)
                                .Query(pesquisa)
                                .Operator(Operator.And)
                                .Boost(10)),
                            s => s.MultiMatch(m => m
                                .Query(pesquisa)
                                .Fields(f => f
                                    .Field(x => x.Nome, 3.0)
                                    .Field(x => x.Descricao)
                                    .Field(x => x.Desenvolvedora)
                                    .Field(x => x.Generos)
                                )
                                .Type(TextQueryType.BestFields)
                                .Fuzziness(Fuzziness.Auto)
                            )
                        ).MinimumShouldMatch(1)))
                .Sort(sort => sort.Descending(SortSpecialField.Score)),
            cancellationToken);

        if (!response.IsValid)
        {
            throw new Exception(
                $"Erro ao pesquisar jogos no OpenSearch: {response.DebugInformation}");
        }

        return response.Hits
            .Where(hit => hit.Source != null)
            .Select(hit => new JogoSearchResultModel(
                hit.Source.Id,
                hit.Source.Nome,
                hit.Source.Preco,
                hit.Source.Descricao,
                hit.Source.Desenvolvedora,
                hit.Source.Generos,
                hit.Source.Plataformas,
                hit.Score ?? 0
            ))
            .ToList();
    }
}
