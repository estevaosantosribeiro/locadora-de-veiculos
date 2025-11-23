using FluentResults;
using FluentValidation;
using LocadoraDeVeiculos.Aplicacao.Compartilhado;
using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Editar;

public class EditarFuncionarioRequestHandler(
    IRepositorioFuncionario repositorioFuncionario,
    IContextoPersistencia contexto,
    IValidator<Funcionario> validador
) : IRequestHandler<EditarFuncionarioRequest, Result<EditarFuncionarioResponse>>
{
    public async Task<Result<EditarFuncionarioResponse>> Handle(
        EditarFuncionarioRequest request, 
        CancellationToken cancellationToken
    )
    {
        var funcionarioSelecionado = await repositorioFuncionario.SelecionarPorIdAsync(request.Id);

        if (funcionarioSelecionado == null) return Result.Fail(ErrorResults.NotFoundError(request.Id));

        funcionarioSelecionado.Nome = request.Nome;
        funcionarioSelecionado.Salario = request.Salario;
        funcionarioSelecionado.DataAdmissao = request.DataAdmissao;

        var resultadoValidacao = await validador.ValidateAsync(funcionarioSelecionado, cancellationToken);

        if (!resultadoValidacao.IsValid)
        {
            var errors = resultadoValidacao.Errors
                .Select(failure => failure.ErrorMessage)
                .ToList();

            return Result.Fail(ErrorResults.BadRequestError(errors));
        }

        var funcionarios = await repositorioFuncionario.SelecionarTodosAsync();

        if (NomeJaExiste(funcionarios, funcionarioSelecionado))
            return Result.Fail(FuncionarioErrorResults.NomeDuplicadoError(funcionarioSelecionado.Nome));

        try
        {
            await repositorioFuncionario.EditarAsync(funcionarioSelecionado);

            await contexto.GravarAsync();
        }
        catch (Exception ex)
        {
            await contexto.RollbackAsync();

            return Result.Fail(ErrorResults.InternalServerError(ex));
        }

        return Result.Ok(new EditarFuncionarioResponse(funcionarioSelecionado.Id));
    }

    private bool NomeJaExiste(List<Funcionario> funcionarios, Funcionario funcionarioAtualizado)
    {
        return funcionarios
            .Where(f => f.Id != funcionarioAtualizado.Id)
            .Any(f => f.Nome.Equals(funcionarioAtualizado.Nome, StringComparison.OrdinalIgnoreCase));
    }
}
