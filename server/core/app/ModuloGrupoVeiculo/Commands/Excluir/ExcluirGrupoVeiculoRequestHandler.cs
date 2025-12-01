using FluentResults;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculo;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.Excluir;

public class ExcluirGrupoVeiculoRequestHandler(
    IContextoPersistencia contexto,
    IRepositorioGrupoVeiculo repositorioGrupoVeiculo
) : IRequestHandler<ExcluirGrupoVeiculoRequest, Result<ExcluirGrupoVeiculoResponse>>
{
    public async Task<Result<ExcluirGrupoVeiculoResponse>> Handle(ExcluirGrupoVeiculoRequest request, CancellationToken cancellationToken)
    {
        var grupoVeiculoSelecionado = await repositorioGrupoVeiculo.SelecionarPorIdAsync(request.Id);

        if (grupoVeiculoSelecionado is null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        try
        {
            await repositorioGrupoVeiculo.ExcluirAsync(grupoVeiculoSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new ExcluirGrupoVeiculoResponse());
    }
}
