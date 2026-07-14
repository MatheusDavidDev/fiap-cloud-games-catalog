using FCG.Catalog.Application.Commands.BibliotecaCommand.AdicionarJogoCommand;
using FCG.Catalog.Core.UnitOfWork;
using FCG.Catalog.Domain.Entities;
using FCG.Contracts;
using MassTransit;
using MediatR;
using System.Threading;

namespace FCG.Catalog.Infra.Rabbitmq.Consumers;

public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
{
    private readonly IMediator _mediator;
    private readonly IUnitOfWork _unitOfWork;


    public PaymentProcessedConsumer(IMediator mediator, IUnitOfWork unitOfWork)
    {
        _mediator = mediator;
        _unitOfWork = unitOfWork;
    }


    public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        var pagamento = context.Message;
        var ordemCompraRepository = _unitOfWork.GetRepository<OrdemCompra>();

        var ordem = await ordemCompraRepository.GetByAsync(predicate: x => x.Id == pagamento.IdOrdemCompra);

        if (ordem == null)
        {
            throw new InvalidOperationException($"Ordem de compra {pagamento.IdOrdemCompra} não encontrada.");
        }
        bool deveAdicionarJogo = false;

        if (pagamento.Status == "Aprovado")
        {
            ordem.Aprovar();
            deveAdicionarJogo = true; 
        }
        else if (pagamento.Status == "Rejeitada")
        {
            ordem.Rejeitar();
        }

        ordemCompraRepository.Update(ordem);
        await _unitOfWork.SaveChanges();

        if (deveAdicionarJogo)
        {
            await _mediator.Send(new AdicionarJogoCommand(pagamento.IdUsuario, pagamento.IdJogo));
        }
    }
}

