using FCG.Catalog.Core.Models;
using FCG.Catalog.Domain.Enuns;

namespace FCG.Catalog.Domain.Entities;

public class OrdemCompra : BaseEntity
{
    public OrdemCompra(Guid idUsuario, Guid idJogo, decimal valor)
    {
        IdUsuario = idUsuario;
        IdJogo = idJogo;
        Valor = valor;
        Status = StatusOrdemCompra.Pendente;
    }

    public Guid IdUsuario { get; private set; }
    public Guid IdJogo { get; private set; }
    public Jogo Jogo { get; private set; }
    public decimal Valor { get; private set; }
    public StatusOrdemCompra Status { get; private set; }

    public void Aprovar()
    {
        Status = StatusOrdemCompra.Aprovada;
    }

    public void Rejeitar()
    {
        Status = StatusOrdemCompra.Rejeitada;
    }

}
