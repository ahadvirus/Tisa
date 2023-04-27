using Microsoft.AspNetCore.Mvc;

namespace Tisa.Store.Web.Models.ViewModels.Entities.Attributes;

public class CreateVM : IndexVM
{
    [FromBody]
    public string Title { get; set; }
}