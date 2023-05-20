using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace Tisa.Store.Web.Ui.Infrastructures.Contracts;

public interface IPageRenderer
{
    /// <summary>
    /// Render page as string
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="page"><see cref="IView"/> the page want to render</param>
    /// <param name="model"><see cref="T"/></param>
    /// <returns><see cref="string"/> Result of razor page return as string</returns>
    Task<string> RenderPageAsync<T>(IRazorPage page, T model);
}