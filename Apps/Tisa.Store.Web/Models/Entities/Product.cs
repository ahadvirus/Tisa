using System.Collections.Generic;

namespace Tisa.Store.Web.Models.Entities;

public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<AttributeProduct> Attributes { get; set; }






}