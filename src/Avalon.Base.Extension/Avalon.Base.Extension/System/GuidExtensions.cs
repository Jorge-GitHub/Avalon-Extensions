namespace Avalon.Base.Extension.System;

public static class GuidExtensions
{
    public static string NewId(bool removeHyphens = true)
    {
        return Guid.NewGuid().ToString(removeHyphens ? "N" : "D");
    }
}
