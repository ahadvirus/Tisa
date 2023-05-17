using System.Collections.Generic;
using Tisa.Store.Web.Ui.Infrastructures.Contracts;

namespace Tisa.Store.Web.Ui.Models.Entities;

public class Role : IEntity<int>
{
    public Role()
    {
        Name = string.Empty;
        Users = new HashSet<RoleUser>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<RoleUser> Users { get; set; }
}