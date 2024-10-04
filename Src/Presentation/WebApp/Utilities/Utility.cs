using System.Globalization;

namespace ShareMarket.WebApp.Utilities;

public static class Utility
{
    public static string HexToRGBA(this string hex, decimal alpha = 0.2M)
    {
        if (hex.Contains('#')) { hex = hex.Replace("#", ""); }
        if (hex.Length != 6) return hex;
        var x = Convert.FromHexString(hex);
        return $"rgba({x[0]}, {x[1]}, {x[2]}, {alpha})";
    }
    public static string ToKMB(this decimal num)
    {
        var info = new CultureInfo("en-IN");
        if (num > 999999999 || num < -999999999)
        {
            return num.ToString("0,,,.###B", info);
        }
        else if (num > 999999 || num < -999999)
        {
            return num.ToString("0,,.##M", info);
        }
        else if (num > 999 || num < -999)
        {
            return num.ToString("0,.#K", info);
        }
        else
        {
            return num.ToString(info);
        }
    }
}
