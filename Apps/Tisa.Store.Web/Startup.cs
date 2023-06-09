﻿using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MySql.Data.MySqlClient;
using Tisa.Store.Web.Controllers;
using Tisa.Store.Web.Data.Contexts;
using Tisa.Store.Web.Infrastructures.Extensions;
using Tisa.Store.Web.Infrastructures.Routes.Constraints;
using Tisa.Store.Web.Infrastructures.Routes.Conventions;
using Tisa.Store.Web.Infrastructures.Validators;

namespace Tisa.Store.Web;

public class Startup
{
    public static void ConfigurationService(IServiceCollection services)
    {
        /*
        string name = nameof(ApplicationContext).Replace(
            nameof(HttpContext).Replace(
                nameof(WebRequestMethods.Http),
                string.Empty
            ),
            string.Empty
        );
        */

        /*
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
        */

        MySqlConnectionStringBuilder connectionStringBuilder = new MySqlConnectionStringBuilder()
        {
            Server = "localhost",
            Port = 3307,
            Database = "TisaStore",
            UserID = "root",
            Password = "123@host.local"
        };

        services.AddDbContext<ApplicationContext>(
            options => options.UseMySQL(connectionStringBuilder.ConnectionString)
            /* .UseInMemoryDatabase(name) */
            /* .UseSqlite(connectionStringBuilder.ConnectionString) */
        );

        services.AddTransient<ValidatorFactory>(provider =>
            new ValidatorFactory(typeof(Validator<>), provider)
        );

        services.Configure<JsonOptions>(options => { options.SerializerOptions.PropertyNameCaseInsensitive = false; });

        services.AddAutoMapper(typeof(Startup).Assembly);

        services.AddControllers(options =>
            {
                System.Type homeController = typeof(HomeController);

                string namespaceTokenValue =
                    !string.IsNullOrWhiteSpace(homeController.Namespace)
                        ? homeController.Namespace
                        : string.Empty;

                options.Conventions.Insert(0, new NamespaceTokenConvention(namespaceTokenValue));
                options.Conventions.Insert(1, new ControllerTokenConvention());
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

        services.AddRouting(options =>
        {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;

            options.ConstraintMap.Add(
                nameof(Models.Entities.Entity)
                    .ToLower(),
                typeof(EntityConstraint)
            );
            
            options.ConstraintMap.Add(
                nameof(Models.Entities.AttributeEntity)
                    .ToLower(),
                typeof(AttributeEntityConstraint)
            );
            
            options.ConstraintMap.Add(
                nameof(Models.Entities.AttributeEntityValidator)
                    .ToLower(),
                typeof(AttributeEntityValidatorConstraint)
            );
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