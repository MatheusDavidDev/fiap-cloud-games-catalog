namespace FCG.Catalog.Application.Queries.DTOs;

public class JogoDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string CategoriaNome { get; set; }
    public decimal PrecoOriginal { get; set; }
    public string PrecoOriginalFormatado => PrecoOriginal == 0 ? "Gratuito" : PrecoOriginal.ToString("C");
}
