using LocadoraDeVeiculos.Dominio.ModuloFuncionario;
using LocadoraDeVeiculos.Infraestrutura.Orm.Compartilhado;
using LocadoraDeVeiculos.Infraestrutura.Orm.ModuloFuncionario;
using LocadoraDeVeiculos.Testes.Integracao.Compartilhado;

namespace LocadoraDeVeiculos.Testes.Integracao.ModuloFuncionario;

[TestClass]
[TestCategory("Testes de Integração - Módulo de Funcionários")]
public sealed class RepositorioFuncionarioEmOrmTests
{
    private LocadoraDeVeiculosDbContext dbContext;
    private RepositorioFuncionarioEmOrm repositorioFuncionario;

    [TestInitialize]
    public void ConfigurarTestes()
    {
        dbContext = LocadoraDeVeiculosDbContextFactory.CriarDbContext();

        repositorioFuncionario = new RepositorioFuncionarioEmOrm(dbContext);
    }

    [TestMethod]
    public async Task Deve_Cadastrar_Funcionario_Corretamente()
    {
        // Arrange
        var funcionario = new Funcionario(
            "Tiago Rech da Silva",
            4500,
            new DateTime(2001, 12, 1)
        );

        // Act
        await repositorioFuncionario.InserirAsync(funcionario);
        dbContext.SaveChanges();

        // Assert
        var registroSelecionado = await repositorioFuncionario.SelecionarPorIdAsync(funcionario.Id);

        Assert.AreEqual(funcionario, registroSelecionado);
    }
}
