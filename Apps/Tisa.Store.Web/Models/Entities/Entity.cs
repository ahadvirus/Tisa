using System.Collections.Generic;

namespace Tisa.Store.Web.Models.Entities;

public class Entity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<AttributeEntity> Attributes { get; set; }






}