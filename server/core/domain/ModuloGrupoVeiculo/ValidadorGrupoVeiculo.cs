using FluentValidation;

namespace LocadoraDeVeiculos.Dominio.ModuloGrupoVeiculo;

public class ValidadorGrupoVeiculo : AbstractValidator<GrupoVeiculo>
{
    public ValidadorGrupoVeiculo()
    {
        RuleFor(x => x.Nome)
            .NotEmpty().WithMessage("O nome do grupo de veículo é obrigatório")
            .Length(3, 100).WithMessage("O nome do grupo de veículo deve ter entre 3 e 100 caracteres");
    }
}
