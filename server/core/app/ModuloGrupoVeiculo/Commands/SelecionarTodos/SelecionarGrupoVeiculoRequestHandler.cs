using FluentResults;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculo;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.SelecionarTodos;

public class SelecionarGrupoVeiculoRequestHandler(
    IRepositorioGrupoVeiculo repositorioGrupoVeiculo
) : IRequestHandler<SelecionarGrupoVeiculoRequest, Result<SelecionarGrupoVeiculoResponse>>
{
    public async Task<Result<SelecionarGrupoVeiculoResponse>> Handle(
        SelecionarGrupoVeiculoRequest request, CancellationToken cancellationToken)
    {
        var registros = await repositorioGrupoVeiculo.SelecionarTodosAsync();

        var response = new SelecionarGrupoVeiculoResponse
        {
            QuantidadeRegistros = registros.Count,
            Registros = registros
                .Select(r => new SelecionarGrupoVeiculoDto(r.Id, r.Nome))
        };

        return Result.Ok(response);
    }
}
