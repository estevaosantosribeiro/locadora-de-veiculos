namespace LocadoraDeVeiculos.Aplicacao.ModuloFuncionario.Commands.SelecionarPorId;

public record SelecionarFuncionarioPorIdResponse(Guid Id, string Nome, decimal Salario, DateTime DataAdmissao);
