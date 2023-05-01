using System.ComponentModel;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Models.ViewModels.Validators;

[DisplayName(nameof(Validator) + nameof(IndexVM))]
public class IndexVM
{
    public IndexVM()
    {
        Name = string.Empty;
        Description = string.Empty;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}