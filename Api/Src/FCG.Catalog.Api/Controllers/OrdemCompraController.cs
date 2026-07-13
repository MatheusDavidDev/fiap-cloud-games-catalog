using FCG.Catalog.Application.Commands.OrdemCompraCommand.CriarOrdemCompraCommand;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FCG.Catalog.Api.Controllers;


[ApiController]
[Route("ordens-compra")]
public class OrdemCompraController : ControllerBase
{
    private readonly IMediator _mediator;
    //private readonly IBibliotecaQueryService _queryService;

    public OrdemCompraController(IMediator mediator /*, IBibliotecaQueryService queryService*/)
    {
        _mediator = mediator;
        //_queryService = queryService;
    }

    [HttpPost("comprar{id}")]
    public async Task<IActionResult> Comprar(Guid id, Guid idJogo)
    {
        await _mediator.Send(new CriarOrdemCompraCommand(id, idJogo));
        return NoContent();
    }
}
