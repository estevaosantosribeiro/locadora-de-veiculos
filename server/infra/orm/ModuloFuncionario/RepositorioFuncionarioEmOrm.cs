using LocadoraDeVeiculos.Dominio.Compartilhado;
using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloFuncionario;

public class RepositorioFuncionarioEmOrm : RepositorioBase<Funcionario>, IRepositorioFuncionario
{
    public RepositorioFuncionarioEmOrm(IContextoPersistencia context) : base(context)
    {
    }
}
