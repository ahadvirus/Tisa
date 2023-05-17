using Microsoft.AspNetCore.Builder;

namespace Tisa.Store.Web.Ui;

public static class Program
{
    public static void Main(string[] args)
    {

        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        Startup.ConfigurationService(services: builder.Services, configuration: builder.Configuration);

        WebApplication app = builder.Build();

        // Configure the HTTP request pipeline.
        Startup.Configuration(app: app);

        app.Run();
    }
}