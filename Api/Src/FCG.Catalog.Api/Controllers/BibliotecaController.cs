using FCG.Catalog.Application.Commands.BibliotecaCommand.AdicionarJogoCommand;
using FCG.Catalog.Application.Commands.OrdemCompraCommand.CriarOrdemCompraCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Catalog.Api.Controllers;

[ApiController]
[Route("Bibliotecas")]
public class BibliotecaController : ControllerBase
{
    private readonly IMediator _mediator;
    //private readonly IBibliotecaQueryService _queryService;

    public BibliotecaController(IMediator mediator /*, IBibliotecaQueryService queryService*/)
    {
        _mediator = mediator;
        //_queryService = queryService;
    }

    [HttpPost("{id}")]
    public async Task<IActionResult> AdicionarJogo(Guid id, Guid idJogo)
    {
        await _mediator.Send(new CriarOrdemCompraCommand(id, idJogo));
        return NoContent();
    }
}
