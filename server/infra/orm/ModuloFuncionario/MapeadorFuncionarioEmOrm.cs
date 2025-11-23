using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocadoraDeVeiculos.Infraestrutura.Orm.ModuloFuncionario;

public class MapeadorFuncionarioEmOrm : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.Property(x => x.Id)
            .ValueGeneratedNever()
            .IsRequired();

        builder.Property(x => x.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Salario)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(x => x.DataAdmissao)
            .IsRequired();
    }
}
