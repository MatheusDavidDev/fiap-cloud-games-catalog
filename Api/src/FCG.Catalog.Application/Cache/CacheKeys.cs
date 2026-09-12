namespace FCG.Catalog.Application.Cache;

public abstract class CacheKeys
{
    public const string TodosJogos = "jogos:todos";
    public static string Jogo(Guid id)
        => $"jogo:{id}";
}
