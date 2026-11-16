using FCG.Catalog.Core.UnitOfWork;
using FCG.Catalog.Domain.Entities;
using FCG.Catalog.Domain.Enuns;
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
        var ordemCompraRepository = _unitOfWork.GetRepository<OrdemCompra>();

        var jogo = await jogoRepository.GetByAsync(predicate: x => x.Id == request.IdJogo, cancellationToken: cancellationToken);

        if (jogo is null)
            throw new Exception("Jogo não existe.");

        var existeOrdem = await ordemCompraRepository.GetByAsync(
            predicate: x => x.IdJogo == request.IdJogo && x.IdUsuario == request.IdUsuario, cancellationToken: cancellationToken);

        if (existeOrdem is not null && existeOrdem.Status == StatusOrdemCompra.Pendente)
            throw new Exception("Já existe uma ordem de compra pendente para este jogo.");

        var biblioteca = await bibliotecaRepository.GetByAsync(
            predicate: x => x.IdUsuario == request.IdUsuario,
            include: x => x.Include(i => i.JogosBiblioteca), cancellationToken: cancellationToken);

        if (biblioteca is null)
        {
            biblioteca = new Biblioteca(request.IdUsuario);
            await bibliotecaRepository.AddAsync(biblioteca, cancellationToken);
        }

        if (biblioteca.JogosBiblioteca.Any(x => x.IdJogo == request.IdJogo))
            throw new Exception("O Jogo já foi adicionado à biblioteca anteriormente.");

        var ordemCompra = new OrdemCompra(request.IdUsuario, jogo.Id, jogo.Preco);

        await ordemCompraRepository.AddAsync(ordemCompra, cancellationToken);

        await _unitOfWork.SaveChanges();

        //Enviando evento para o serviço de pagamento
        await _publishEndpoint.Publish(
            new OrderPlacedEvent(
                ordemCompra.Id, 
                ordemCompra.IdUsuario, 
                ordemCompra.IdJogo, 
                ordemCompra.Valor, 
                DateTime.UtcNow), cancellationToken);
    }
}
