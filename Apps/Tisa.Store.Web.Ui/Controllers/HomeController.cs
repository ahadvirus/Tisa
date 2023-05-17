using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Ui.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
