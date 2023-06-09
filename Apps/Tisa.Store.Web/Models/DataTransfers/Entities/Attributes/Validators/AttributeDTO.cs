﻿using System;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Models.DataTransfers.Entities.Attributes.Validators;

public class AttributeDTO : IAttributeDTO
{
    public AttributeDTO()
    {
        Name = string.Empty;
        Type = string.Empty;
    }
    
    public int Id { get; set; }
    public string Name { get; set; }
    public int TypeId { get; set; }
    public string Type { get; set; }
     public Type? GetType
    {
        get
        {
            return System.Type.GetType(
                string.Format("{0}.{1}", nameof(System), Type)
            );
        }
    }
}