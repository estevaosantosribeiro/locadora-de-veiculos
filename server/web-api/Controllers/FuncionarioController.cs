using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Editar;
using LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Inserir;
using LocadoraDeVeiculos.WebApi.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LocadoraDeVeiculos.WebApi.Controllers;

[ApiController]
[Authorize]
[Route("api/funcionarios")]
public class FuncionarioController(IMediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(typeof(InserirFuncionarioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Inserir(InserirFuncionarioRequest request)
    {
        var resultado = await mediator.Send(request);

        return resultado.ToHttpResponse();
    }

    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(EditarFuncionarioResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> Editar(Guid id, EditarFuncionarioRequest request)
    {
        var editarRequest = new EditarFuncionarioRequest(
            id,
            request.Nome,
            request.Salario,
            request.DataAdmissao
        );

        var resultado = await mediator.Send(editarRequest);

        return resultado.ToHttpResponse();
    }
}
