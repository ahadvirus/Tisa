namespace Tisa.Store.Web.Infrastructures.Extensions;

public static class ApplyString
{
    public static string RemoveNumber(this string value)
    {
        return value;
    }

    public static string GetName(this System.Type systems)
    {
        return systems.GetName();
    }
}