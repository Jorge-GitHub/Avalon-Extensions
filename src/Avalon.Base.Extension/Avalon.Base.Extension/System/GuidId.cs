namespace Avalon.Base.Extension.System;

public static class GuidId
{
    public static string NewId(bool removeHyphens = true)
    {
        return Guid.NewGuid().ToString(removeHyphens ? "N" : "D");
    }
}
