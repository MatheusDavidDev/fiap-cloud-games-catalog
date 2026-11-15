namespace FCG.Catalog.Application.Interfaces.Models;

public record JogoDetalhesModel(
    Guid JogoId,
    string? Descricao,
    string? Desenvolvedora,
    List<string> Generos,
    List<string> Plataformas,
    string? RequisitosMinimos
);
