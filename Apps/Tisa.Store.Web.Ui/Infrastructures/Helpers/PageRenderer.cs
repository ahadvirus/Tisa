using System;
using System.Diagnostics;
using System.IO;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Infrastructures.Helpers;

public class PageRenderer : IPageRenderer
{
    private IServiceProvider ServiceProvider { get; }
    private ITempDataProvider TempDataProvider { get; }
    private IRazorViewEngine ViewEngine { get; }
    private IRazorPageActivator PageActivator { get; }

    public PageRenderer(
        IServiceProvider serviceProvider,
        ITempDataProvider tempDataProvider,
        IRazorViewEngine viewEngine,
        IRazorPageActivator pageActivator
        )
    {
        ServiceProvider = serviceProvider;
        TempDataProvider = tempDataProvider;
        ViewEngine = viewEngine;
        PageActivator = pageActivator;
    }

    public async Task<string> RenderPageAsync<T>(IRazorPage page, T model)
    {
        string result;

        await using (StringWriter writer = new StringWriter())
        {
            Debug.WriteLine(message: string.Format(format:"\n{0}\n", args: new object?[]{ page.Path }));
            page.ViewContext = await GetViewContext(page: new RazorView(
                    viewEngine: ViewEngine,
                    pageActivator: PageActivator,
                    viewStartPages: new IRazorPage[] { },
                    razorPage: page,
                    htmlEncoder: HtmlEncoder.Default,
                    diagnosticListener: new DiagnosticListener("ViewRenderService")
                    ),
                writer: writer,
                model: model);

            page.Layout = string.Empty;

            await page.ExecuteAsync();

            result = writer.ToString();
        }

        return result;
    }

    private async Task<ViewContext> GetViewContext(IView page, TextWriter writer, object? model)
    {
        ActionContext actionContext = await GetFakeActionContext();
        
        ViewDataDictionary viewData = new ViewDataDictionary(metadataProvider: new EmptyModelMetadataProvider(), modelState: new ModelStateDictionary())
        {
            Model = model
        };

        TempDataDictionary tempData = new TempDataDictionary(context: actionContext.HttpContext, provider: TempDataProvider);

        return new ViewContext(
            actionContext: actionContext,
            view: page,
            viewData: viewData,
            tempData: tempData,
            writer: writer,
            htmlHelperOptions: new HtmlHelperOptions());
    }

    private Task<ActionContext> GetFakeActionContext()
    {
        return Task.Run(() =>
        {
            DefaultHttpContext httpContext = new DefaultHttpContext
            {
                RequestServices = ServiceProvider,
            };

            RouteData routeData = new RouteData();
            ActionDescriptor actionDescriptor = new ActionDescriptor();

            return new ActionContext(httpContext: httpContext, routeData: routeData, actionDescriptor: actionDescriptor);
        });
    }
}