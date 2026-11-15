namespace FCG.Catalog.Api.Controllers.Models;

public record AtualizarJogoModel(
    string Nome,
    decimal Preco,
    Guid? IdCategoria = null,
    string? Descricao = null,
    string? Desenvolvedora = null,
    List<string>? Generos = null,
    List<string>? Plataformas = null,
    string? RequisitosMinimos = null
);
