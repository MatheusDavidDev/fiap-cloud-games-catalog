namespace FCG.Catalog.Infra.OpenSearch.Models;

public class JogoIndexDocument
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public Guid? IdCategoria { get; set; }

    public string? Descricao { get; set; }
    public string? Desenvolvedora { get; set; }
    public List<string> Generos { get; set; } = [];
    public List<string> Plataformas { get; set; } = [];
}
