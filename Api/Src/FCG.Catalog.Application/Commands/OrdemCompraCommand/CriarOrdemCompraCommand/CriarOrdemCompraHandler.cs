using FCG.Catalog.Core.UnitOfWork;
using FCG.Catalog.Domain.Entities;
using FCG.Contracts;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FCG.Catalog.Application.Commands.OrdemCompraCommand.CriarOrdemCompraCommand;

public class CriarOrdemCompraHandler : IRequestHandler<CriarOrdemCompraCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;

    public CriarOrdemCompraHandler(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
    {
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
    }
    public async Task Handle(CriarOrdemCompraCommand request, CancellationToken cancellationToken)
    {
        var bibliotecaRepository = _unitOfWork.GetRepository<Biblioteca>();
        var jogoRepository = _unitOfWork.GetRepository<Jogo>();

        var biblioteca = await bibliotecaRepository.GetByAsync(
            predicate: x => x.IdUsuario == request.IdUsuario,
            include: x => x.Include(i => i.JogosBiblioteca), cancellationToken: cancellationToken);

        var jogo = await jogoRepository.GetByAsync(predicate: x => x.Id == request.IdJogo, cancellationToken: cancellationToken);


        if (biblioteca is null)
        {
            biblioteca = new Biblioteca(request.IdUsuario);
        }

        if (biblioteca.JogosBiblioteca.Any(x => x.IdJogo == request.IdJogo))
            throw new Exception("Jogo já adicionado à biblioteca.");


        if (jogo is null)
            throw new Exception("Jogo não existe.");

        var ordemCompra = new OrdemCompra(request.IdUsuario, jogo.Id, jogo.Preco);

        await _unitOfWork.SaveChanges();

        //Enviando evento para o serviço de pagamento
        //await _publishEndpoint.Publish(
        //    new OrderPlacedEvent(ordemCompra.Id, ordemCompra.IdUsuario, ordemCompra.IdJogo, ordemCompra.Valor, DateTime.UtcNow), cancellationToken);
    }
}
