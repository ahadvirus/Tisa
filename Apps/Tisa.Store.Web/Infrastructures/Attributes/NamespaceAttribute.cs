namespace Tisa.Store.Web.Infrastructures.Attributes;

public class NamespaceAttribute : System.Attribute
{
    public string Default { get; }

    public NamespaceAttribute(string @default)
    {
        Default = @default;
    }
}