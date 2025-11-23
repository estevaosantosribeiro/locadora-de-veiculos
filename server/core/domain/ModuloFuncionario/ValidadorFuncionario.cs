using FluentValidation;

namespace LocadoraDeVeiculos.Dominio.ModuloFuncionario;

public class ValidadorFuncionario : AbstractValidator<Funcionario>
{
    public ValidadorFuncionario()
    {
        RuleFor(f => f.Nome)
            .NotEmpty().WithMessage("O nome do funcionário é obrigatório.")
            .MaximumLength(100).WithMessage("O nome do funcionário não pode exceder 100 caracteres.");
        RuleFor(f => f.Salario)
            .GreaterThan(0).WithMessage("O salário do funcionário deve ser maior que zero.");
        RuleFor(f => f.DataAdmissao)
            .LessThanOrEqualTo(DateTime.Now).WithMessage("A data de admissão não pode ser no futuro.");
    }
}
