using System.Collections.Generic;

namespace Tisa.Store.Web.Ui.Models.Entities;

public class User
{
    public User()
    {
        Username = string.Empty;
        Password = string.Empty;
        Roles = new HashSet<RoleUser>();
    }
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public virtual ICollection<RoleUser> Roles { get; set; }

}