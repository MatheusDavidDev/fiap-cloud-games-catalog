using FCG.Catalog.Core.UnitOfWork;
using MediatR;

namespace FCG.Catalog.Application.Commands.BibliotecaCommand.AdicionarJogoCommand;

public class AdicionarJogoHandler : IRequestHandler<AdicionarJogoCommand>
{
    private readonly IUnitOfWork _unitOfWork;

    public AdicionarJogoHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task Handle(AdicionarJogoCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
