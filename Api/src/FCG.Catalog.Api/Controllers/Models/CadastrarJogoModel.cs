namespace FCG.Catalog.Api.Controllers.Models;

public record CadastrarJogoModel(
    string Nome, 
    decimal Preco, 
    Guid? IdCategoria,
    string? Descricao = null,
    string? Desenvolvedora = null,
    List<string>? Generos = null,
    List<string>? Plataformas = null,
    string? RequisitosMinimos = null);

