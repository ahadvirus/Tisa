using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    // GET
    [HttpGet]
    public Task<IActionResult> Index()
    {
        /*
        JsonNodeOptions options = new JsonNodeOptions()
        {
            PropertyNameCaseInsensitive = false
        };
        
        List<JsonObject> names = new List<JsonObject>();
        foreach (string item in new string[] { "Ahmad", "Amin", "Ahad", "Fatemeh" })
        {
            JsonObject name = new JsonObject(options);
            name.Add(System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(Models.Entities.Attribute.Name)), JsonValue.Create<string>(item));
            names.Add(name);
        }

        JsonObject result = new JsonObject(options);

        result.Add(System.Text.Json.JsonNamingPolicy.CamelCase.ConvertName(nameof(ArrayList).Replace(nameof(Array), string.Empty)), JsonValue.Create(names));

        return Task.FromResult<IActionResult>(Ok(result));
        */
        return Task.FromResult<IActionResult>(Ok("Hello"));
    }
}