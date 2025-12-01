using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloAutenticacao;
using LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculo;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloGrupoVeiculo.Commands.Inserir;

public class InserirGrupoVeiculoRequestHandler(
    IContextoPersistencia contexto,
    IRepositorioGrupoVeiculo repositorioGrupoVeiculo,
    ITenantProvider tenantProvider,
    IValidator<GrupoVeiculo> validador
) : IRequestHandler<InserirGrupoVeiculoRequest, Result<InserirGrupoVeiculoResponse>>
{
    public async Task<Result<InserirGrupoVeiculoResponse>> Handle(
        InserirGrupoVeiculoRequest request, 
        CancellationToken cancellationToken
    )
    {
        var grupoVeiculo = new GrupoVeiculo(request.nome)
        {
            UsuarioId = tenantProvider.UsuarioId.GetValueOrDefault()
        };

        var resultadoValidacao = await validador.ValidateAsync(grupoVeiculo);

        if (!resultadoValidacao.IsValid)
        {
            var erros = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(erros));
        }

        var gruposVeiculoRegistrados = await repositorioGrupoVeiculo.SelecionarTodosAsync();

        if (NomeDuplicado(grupoVeiculo, gruposVeiculoRegistrados))
            return Result.Fail(GrupoVeiculoErrorResults.NomeDuplicadoError(grupoVeiculo.Nome));

        try
        {
            await repositorioGrupoVeiculo.InserirAsync(grupoVeiculo);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new InserirGrupoVeiculoResponse(grupoVeiculo.Id));
    }

    private bool NomeDuplicado(GrupoVeiculo grupoVeiculo, IList<GrupoVeiculo> grupoVeiculos)
    {
        return grupoVeiculos
            .Any(registro => string.Equals(
                registro.Nome,
                grupoVeiculo.Nome,
                StringComparison.CurrentCultureIgnoreCase)
            );
    }
}
