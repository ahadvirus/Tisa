using System;

namespace Tisa.Store.Web.Ui.Infrastructures.Attributes;

/// <summary>
/// To ordering seed class for execution
/// </summary>
public class OrderAttribute : Attribute
{
    public OrderAttribute(long version)
    {
        Version = version;
    }

    /// <summary>
    /// Keep date and time of creation of seed
    /// </summary>
    public long Version { get; }
}