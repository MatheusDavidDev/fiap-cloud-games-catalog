namespace FCG.Catalog.Application.Queries.DTOs;

public class OrdemCompraDto
{
    public Guid Id { get; set; }
    public string NomeJogo { get; set; }
    public decimal Valor { get; set; }
    public string ValorFormatado => Valor == 0 ? "Gratuito" : Valor.ToString("C");
    public string Status { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public string DataCriacaoFormatada => CreatedAt.ToString("dd/MM/yyyy HH:mm:ss");
    public string DataAtualizacaoFormatada => UpdatedAt.ToString("dd/MM/yyyy HH:mm:ss");
}
