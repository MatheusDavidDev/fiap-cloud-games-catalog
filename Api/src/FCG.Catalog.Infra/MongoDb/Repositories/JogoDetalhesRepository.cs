using FCG.Catalog.Application.Interfaces;
using FCG.Catalog.Application.Interfaces.Models;
using FCG.Catalog.Infra.MongoDb.Models;
using MongoDB.Driver;

namespace FCG.Catalog.Infra.MongoDb.Repositories;

public class JogoDetalhesRepository : IJogoDetalhesRepository
{
    private readonly IMongoCollection<JogoDetalhesDocument> _collection;

    public JogoDetalhesRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<JogoDetalhesDocument>("JogoDetalhes");
    }

    public async Task SalvarAsync(Guid jogoId, string? descricao, string? desenvolvedora, List<string>? generos,
        List<string>? plataformas, string? requisitosMinimos, CancellationToken cancellationToken = default)
    {
        var documento = new JogoDetalhesDocument
        {
            JogoId = jogoId,
            Descricao = descricao,
            Desenvolvedora = desenvolvedora,
            Generos = generos ?? [],
            Plataformas = plataformas ?? [],
            RequisitosMinimos = requisitosMinimos
        };

        var filtro = Builders<JogoDetalhesDocument>.Filter.Eq(x => x.JogoId, jogoId);

        await _collection.ReplaceOneAsync(filtro, documento, new ReplaceOptions { IsUpsert = true }, cancellationToken);
    }

    public async Task<JogoDetalhesModel?> ObterPorJogoIdAsync(Guid jogoId, CancellationToken cancellationToken = default)
    {
        var documento = await _collection.Find(x => x.JogoId == jogoId)
            .FirstOrDefaultAsync(cancellationToken);

        if (documento is null)
            return null;

        return new JogoDetalhesModel(
            documento.JogoId,
            documento.Descricao,
            documento.Desenvolvedora,
            documento.Generos,
            documento.Plataformas,
            documento.RequisitosMinimos
        );
    }
}
