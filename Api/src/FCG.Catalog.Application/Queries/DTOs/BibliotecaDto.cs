namespace FCG.Catalog.Application.Queries.DTOs;

public class BibliotecaDto
{
    public Guid Id { get; set; }

    public List<JogosDto> Jogos { get; set; }
}

public class JogosDto
{
    public Guid IdJogo { get; set; }
    public string Nome { get; set; }
    public DateTime DataAdicionadoEm { get; set; }
    public string DataAdicionadoEmFormatado => DataAdicionadoEm.ToString("dd/MM/yyyy");
}
