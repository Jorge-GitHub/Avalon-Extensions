namespace Avalon.Base.Extension.Types.IntegerExtensions;

public static class IntegerFormatExtensions
{
    public static int NormalizePositive(this int value, int defaultValue = 0)
    {
        return value > 0 ? value : defaultValue;
    }
}
