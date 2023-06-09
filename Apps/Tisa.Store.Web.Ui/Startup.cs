﻿using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Localization;
using System.Globalization;
using Tisa.Store.Web.Ui.Data.Contexts;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts;
using Tisa.Store.Web.Ui.Data.Repositories.Contracts.Apis;
using Tisa.Store.Web.Ui.Data.Repositories.Persistences;
using Tisa.Store.Web.Ui.Data.Repositories.Persistences.Apis;
using Tisa.Store.Web.Ui.Infrastructures.Configurations;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;
using Tisa.Store.Web.Ui.Infrastructures.Extensions;
using Tisa.Store.Web.Ui.Infrastructures.Helpers;

namespace Tisa.Store.Web.Ui;

public static class Startup
{
    public static void ConfigurationService(IServiceCollection services, IConfiguration configuration)
    {
        string localizationResourcesPath = nameof(LocalizationOptions.ResourcesPath)
            .Replace(oldValue: nameof(System.IO.Path), newValue: string.Empty);

        services.AddLocalization(setupAction: options => options.ResourcesPath = localizationResourcesPath);

        services.AddControllersWithViews()
            .AddViewLocalization(setupAction: options => options.ResourcesPath = localizationResourcesPath)
            .AddDataAnnotationsLocalization();


        services.AddScoped<ApiContext>();

        services.AddScoped<IApiTypeRepository, ApiTypeRepository>();

        services.AddScoped<IApiAttributeRepository, ApiAttributeRepository>();

        services.AddScoped<ITypeRepository, TypeRepository>();

        services.AddScoped<IAttributeRepository, AttributeRepository>();

        services.AddScoped<IPageRenderer, PageRenderer>();

        services.Configure<RequestLocalizationOptions>(configureOptions: LocalizationOption);

        services.AddRouting(configureOptions: options =>
        {
            options.LowercaseQueryStrings = true;
            options.LowercaseUrls = true;
        });

        ApiOption? option =
            configuration.GetSection(key:
                    string.Format(
                        format: "{0}:{1}",
                        args: new object?[] { nameof(Api), nameof(Option) }
                    )
                )
                .Get<ApiOption>();

        if (option != null)
        {
            services.AddScoped<ApiOption>(_ => option);
        }

        MySqlConnection? connection = configuration.GetSection(
            key: string.Format(
                format: "{0}:{1}",
                args: new object?[] { nameof(DbContext.Database), nameof(MySql) }
            )
        ).Get<MySqlConnection>();

        if (connection != null)
        {
            services.AddDbContext<ApplicationContext>(optionsAction: options =>
                options.UseMySQL(connectionString: connection.String())
            );
        }
    }

    public static void Configuration(WebApplication app)
    {
        app.UseApplySeedFromAssembly(typeof(Startup).Assembly);
        
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseRequestLocalization(optionsAction: LocalizationOption);

        app.UseRequestCulture();

        app.UseHttpsRedirection();

        app.UseServiceExtension();

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "areas",
                pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );

            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"><see cref="RequestLocalizationOptions"/></param>
    private static void LocalizationOption(RequestLocalizationOptions options)
    {
        string persianCulture = "fa-IR";
        string englishCulture = "en-US";

        CultureInfo[] supportedCultures = new CultureInfo[]
        {
            new CultureInfo(name: englishCulture),
            new CultureInfo(name: persianCulture)
        };

        options.DefaultRequestCulture = new RequestCulture(culture: persianCulture, uiCulture: persianCulture);
        options.SupportedCultures = supportedCultures;
        options.SupportedUICultures = supportedCultures;
        options.SetDefaultCulture(persianCulture);
    }
}