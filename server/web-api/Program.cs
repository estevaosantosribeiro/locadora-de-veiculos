
using LocadoraDeVeiculos.WebApi.Config;
using Serilog;

namespace LocadoraDeVeiculos.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.ConfigureSerilog(builder.Logging, builder.Configuration);

            builder.Services.ConfigureDbContext(builder.Configuration, builder.Environment);

            builder.Services.ConfigureRepositories();
            builder.Services.ConfigureFluentValidation();
            builder.Services.ConfigureMediatR();

            builder.Services.ConfigureIdentityProviders();
            builder.Services.ConfigureJwtAuthentication(builder.Configuration);

            builder.Services.ConfigureControllersWithFilters();

            builder.Services.ConfigureOpenApiAuthHeaders();

            builder.Services.ConfigureCorsPolicy(builder.Environment, builder.Configuration);

            var app = builder.Build();

            app.UseGlobalExceptionHandler();

            app.AutoMigrateDatabase();

            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            try
            {
                app.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal("Ocorreu um erro fatal durante a execução da aplicação: {@Excecao}", ex);
            }
        }
    }
}
