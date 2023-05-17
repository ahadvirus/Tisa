namespace Tisa.Store.Web.Ui.Models.Entities;

public class RoleUser
{
    public RoleUser()
    {
        Role = new Role();
        User = new User();
    }
    public int Id { get; set; }
    public int RoleId { get; set; }
    public virtual Role Role { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
}