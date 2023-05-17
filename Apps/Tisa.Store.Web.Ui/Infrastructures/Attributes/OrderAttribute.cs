using System;

namespace Tisa.Store.Web.Infrastructures.Attributes;

public class OrderAttribute : Attribute
{
    public OrderAttribute(int number)
    {
        Number = number;
    }

    public int Number { get; }
}