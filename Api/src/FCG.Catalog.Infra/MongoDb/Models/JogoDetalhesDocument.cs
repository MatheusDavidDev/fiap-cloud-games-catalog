using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FCG.Catalog.Infra.MongoDb.Models;

public class JogoDetalhesDocument
{
    [BsonId]
    [BsonGuidRepresentation(GuidRepresentation.Standard)]
    public Guid JogoId { get; set; }

    public string? Descricao { get; set; }

    public string? Desenvolvedora { get; set; }

    public List<string> Generos { get; set; } = [];

    public List<string> Plataformas { get; set; } = [];

    public string? RequisitosMinimos { get; set; }
}
