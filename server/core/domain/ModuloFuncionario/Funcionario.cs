using LocadoraDeVeiculos.Dominio.Compartilhado;

namespace LocadoraDeVeiculos.Dominio.ModuloFuncionario;

public class Funcionario : EntidadeBase
{
    public string Nome { get; set; }
    public decimal Salario { get; set; }
    public DateTime DataAdmissao { get; set; }

    public Funcionario() { }

    public Funcionario(string nome, decimal salario, DateTime dataAdmissao) : this()
    {
        this.Nome = nome;
        this.Salario = salario;
        this.DataAdmissao = dataAdmissao;
    }
}
