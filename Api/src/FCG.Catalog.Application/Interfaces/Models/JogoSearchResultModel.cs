namespace FCG.Catalog.Application.Interfaces.Models;

public record JogoSearchResultModel(
    Guid Id,
    string Nome,
    decimal Preco,
    string? Descricao,
    string? Desenvolvedora,
    List<string> Generos,
    List<string> Plataformas,
    double Score
);
