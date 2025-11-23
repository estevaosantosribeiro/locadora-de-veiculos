using FluentResults;
using MediatR;

namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.Editar;

public record EditarFuncionarioRequest(
    Guid Id,
    string Nome, 
    decimal Salario, 
    DateTime DataAdmissao, 
    string UserName, 
    string Email, 
    string Password
) : IRequest<Result<EditarFuncionarioResponse>>;
