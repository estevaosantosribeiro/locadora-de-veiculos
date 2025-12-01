using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculo;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.Editar;

public class EditarGrupoVeiculoRequestHandler(
    IContextoPersistencia contexto,
    IRepositorioGrupoVeiculo repositorioGrupoVeiculo,
    IValidator<GrupoVeiculo> validator
) : IRequestHandler<EditarGrupoVeiculoRequest, Result<EditarGrupoVeiculoResponse>>
{
    public async Task<Result<EditarGrupoVeiculoResponse>> Handle(EditarGrupoVeiculoRequest request, CancellationToken cancellationToken)
    {
        var grupoVeiculoSelecionado = await repositorioGrupoVeiculo.SelecionarPorIdAsync(request.Id);

        if (grupoVeiculoSelecionado == null)
            return Result.Fail(ErrorResults.NotFoundError(request.Id));

        grupoVeiculoSelecionado.Nome = request.Nome;

        var resultadoValidacao = await validator.ValidateAsync(grupoVeiculoSelecionado, cancellationToken);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(fail => fail.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var grupoVeiculos = await repositorioGrupoVeiculo.SelecionarTodosAsync();

        if (NomeDuplicado(grupoVeiculoSelecionado, grupoVeiculos))
            return Result.Fail(GrupoVeiculoErrorResults.NomeDuplicadoError(grupoVeiculoSelecionado.Nome));

        try
        {
            await repositorioGrupoVeiculo.EditarAsync(grupoVeiculoSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }


        return Result.Ok(new EditarGrupoVeiculoResponse(grupoVeiculoSelecionado.Id));
    }

    private bool NomeDuplicado(GrupoVeiculo grupoVeiculo, IList<GrupoVeiculo> grupoVeiculos)
    {
        return grupoVeiculos
            .Where(r => r.Id != grupoVeiculo.Id)
            .Any(registro => string.Equals(
                registro.Nome,
                grupoVeiculo.Nome,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
}
