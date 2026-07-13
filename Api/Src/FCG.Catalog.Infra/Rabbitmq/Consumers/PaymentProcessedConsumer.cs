using FCG.Catalog.Application.Commands.BibliotecaCommand.AdicionarJogoCommand;
using FCG.Contracts;
using MassTransit;
using MediatR;
using System.Runtime.Intrinsics.Arm;

namespace FCG.Catalog.Infra.Rabbitmq.Consumers;

public class PaymentProcessedConsumer : IConsumer<PaymentProcessedEvent>
{
    private readonly IMediator _mediator;


    public PaymentProcessedConsumer(IMediator mediator)
    {
        _mediator = mediator;
    }


    public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
    {
        var pagamento = context.Message;


        if (pagamento.Status == "Aprovado")
        {
            await _mediator.Send(new AdicionarJogoCommand(pagamento.IdUsuario, pagamento.IdJogo));
        }
    }
}
