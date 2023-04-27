using System.ComponentModel;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Models.ViewModels.Attributes;

[DisplayName(nameof(Attribute) + nameof(Entities.IndexVM))]
public class IndexVM
{
    public IndexVM()
    {
        Title = string.Empty;
        Display = string.Empty;
        Type = string.Empty;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Display { get; set; }
    public string Type { get; set; }
}