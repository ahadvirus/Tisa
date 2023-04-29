using System.ComponentModel;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Models.ViewModels.Types;

[DisplayName(nameof(Type) + nameof(IndexVM))]
public class IndexVM
{
    public IndexVM()
    {
        Name = string.Empty;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
}