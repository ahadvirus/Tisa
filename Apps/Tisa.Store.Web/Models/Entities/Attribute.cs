﻿using System.Collections.Generic;

namespace Tisa.Store.Web.Models.Entities;

public class Attribute
{
    public Attribute()
    {
        Name = string.Empty;
        Discription = string.Empty;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Discription { get; set; }
    public int TypeId { get; set; }
    public virtual Type Type { get; set; }

    public virtual ICollection<AttributeEntity> Entites { get; set; }
}