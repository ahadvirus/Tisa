using System.Collections.Generic;
using System.ComponentModel;
using Tisa.Store.Web.Models.Entities;

namespace Tisa.Store.Web.Models.ViewModels.Entities
{
    [DisplayName(nameof(Entity) + nameof(IndexVM))]
    public class IndexVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ViewModels.Attributes.IndexVM> Attributes { get; set; }
    }
}
