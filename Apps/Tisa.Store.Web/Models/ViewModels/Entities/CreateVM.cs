using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Models.ViewModels.Entities;

[DisplayName(nameof(Entity) + nameof(CreateVM))]
public class CreateVM
{
    [Required]
    public string Name { get; set; }
    [Required]
    public List<string> Attributes { get; set; }
}