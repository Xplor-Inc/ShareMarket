using System.Globalization;

namespace ShareMarket.Core.Extensions;

public static class DecimalExtensions
{
    readonly static CultureInfo Culture = new ("en-IN", true);
    readonly static string format = "₹ ###,##0.##";
    public static string ToCString(this decimal? d)
    {
        return d.HasValue ? d.Value.ToString(format, Culture) : string.Empty;
    }
    public static string ToCString(this decimal d)
    {
        return d.ToString(format, Culture);
    }
    public static string ToCString(this decimal d, int decimalPoint)
    {
        return d.ToFixed(decimalPoint).ToString(format, Culture);
    }

    public static string ToCString(this double d)
    {
        return d.ToString(format, Culture);
    }
    public static decimal ToFixed(this decimal d, int decimalPoint = 2)
    {
        return decimal.Round(d, decimalPoint, MidpointRounding.AwayFromZero);
    }
    public static string ToFixedString(this decimal d, int decimalPoint = 2)
    {
        return decimal.Round(d, decimalPoint, MidpointRounding.AwayFromZero).ToString(format, Culture); ;
    }
}