using FCG.Catalog.Application.Commands.OrdemCompraCommand.CriarOrdemCompraCommand;
using FCG.Catalog.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FCG.Catalog.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/ordens-compra")]
public class OrdemCompraController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IOrdemCompraQueryService _queryService;

    public OrdemCompraController(IMediator mediator , IOrdemCompraQueryService queryService)
    {
        _mediator = mediator;
        _queryService = queryService;
    }

    [HttpPost("comprar")]
    public async Task<IActionResult> Comprar(Guid idJogo)
    {
        var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (!Guid.TryParse(idUsuarioClaim, out Guid idUsuario))
        {
            return Unauthorized("ID do usuário inválido ou não encontrado no token.");
        }

        await _mediator.Send(new CriarOrdemCompraCommand(idUsuario, idJogo));
        return NoContent();
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> OrdemPorId(Guid id)
    {
        var result = await _queryService.ObterOrdemPorIdAsync(id, CancellationToken.None);
        return Ok(result);
    }

    [HttpGet("usuario/{id:guid}")]
    public async Task<IActionResult> OrdensPorIdUsuarioAsync(Guid id)
    {
        var result = await _queryService.ObterOrdemUsuarioAsync(id, CancellationToken.None);
        return Ok(result);
    }


}
