using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using TaskManagerBackend.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona a configuração do appsettings.json
        builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

        // Configura o serviço DbContext
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        var app = builder.Build();
        app.MapControllers();
        app.Run();
    }
}
public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }
}


