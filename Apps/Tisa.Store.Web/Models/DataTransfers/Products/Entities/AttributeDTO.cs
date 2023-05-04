using System;
using Tisa.Store.Web.Infrastructures.Contracts.DataTransfers;

namespace Tisa.Store.Web.Models.DataTransfers.Products.Entities;

public class AttributeDTO : IAttributeDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
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