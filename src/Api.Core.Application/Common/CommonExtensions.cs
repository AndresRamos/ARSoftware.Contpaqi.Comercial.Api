namespace Api.Core.Application.Common;

public static class CommonExtensions
{
    /// <summary>
    ///     https://stackoverflow.com/questions/15341028/check-if-a-property-exists-in-a-class
    /// </summary>
    public static bool HasProperty(this Type obj, string propertyName)
    {
        return obj.GetProperty(propertyName) != null;
    }
}
