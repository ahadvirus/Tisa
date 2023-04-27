using System;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Tisa.Store.Web.Controllers;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Infrastructures.Extensions;
using Tisa.Store.Web.Infrastructures.Routes;

namespace Tisa.Store.Web;

public class Startup
{
    public static void ConfigurationService(IServiceCollection services)
    {
        string name = nameof(ApplicationContext).Replace(
            nameof(HttpContext).Replace(
                nameof(WebRequestMethods.Http),
                string.Empty
            ),
            string.Empty
        );

        SqliteConnectionStringBuilder connectionStringBuilder = new SqliteConnectionStringBuilder()
        {
            DataSource = System.IO.Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                string.Format(
                    "{0}.db",
                    name
                )
            )
        };

        services.AddDbContext<ApplicationContext>(
            options => options.UseInMemoryDatabase(name) /* .UseSqlite(connectionStringBuilder.ConnectionString) */);

        services.Configure<JsonOptions>(options => { options.SerializerOptions.PropertyNameCaseInsensitive = false; });

        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddControllers(options =>
            {
                Type homeController = typeof(HomeController);
                
                string namespaceTokenValue =
                    !string.IsNullOrWhiteSpace(homeController.Namespace)
                        ? homeController.Namespace
                        : string.Empty;

                options.Conventions.Insert(0, new NamespaceToken(namespaceTokenValue));
                options.Conventions.Add(new ControllerNameDocumentationConvention());
            })
            .AddJsonOptions(options => { options.JsonSerializerOptions.PropertyNameCaseInsensitive = false; });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.GetCustomAttributes()
                .Where(attribute => attribute.GetType() == typeof(DisplayNameAttribute))
                .Select(attribute => ((DisplayNameAttribute)attribute).DisplayName)
                .FirstOrDefault());
        });
    }

    public static void Configuration(WebApplication app)
    {
        app.UseApplySeedFromAssembly(typeof(Startup).Assembly);

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}