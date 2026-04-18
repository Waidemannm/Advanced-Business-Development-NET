using Microsoft.EntityFrameworkCore;
using OnlineInfrastructure.Persistence;
namespace OnlineSore.Domain;

/// <summary>
/// Ponto de entrada da aplicação.
/// Configura e executa o pipeline da API ASP.NET Core.
/// </summary>
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Registra os controllers da API
        builder.Services.AddControllers();

        // AddScoped: uma instância por requisição HTTP (guardado)
        
        // AddTransient: nova instância a cada resolução
        // AddSingleton: uma única instância durante toda a vida da aplicação
        //builder.Services.AddScoped<IUserService, UserService>();

        //Registrando banco de dados no contexto de injecao de dependencia
        builder.Services.AddDbContext<OnlineStoreContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("OnlineStoreContextOracle");
            
            
            options.UseOracle(connectionString);
            
        });

        // OpenAPI/Swagger - documentação da API
        builder.Services.AddOpenApi();

        var app = builder.Build();

        // Pipeline de requisições
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi(); // /openapi/v1.json
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}