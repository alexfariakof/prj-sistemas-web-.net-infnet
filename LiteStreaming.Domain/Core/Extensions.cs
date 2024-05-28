using System.Globalization;

namespace Domain.Core;
public static class Extensions
{
    public const string DefaultCultureInfo = "pt-BR";

    public static Guid ToGuid(this string srtToConvert)
    {
        return new Guid(srtToConvert);        
    }

    public static Guid ToGuid(this object objToConvert)
    {
        Guid objConvert;
        Guid.TryParse(objToConvert.ToString(), out objConvert);
        return objConvert;
    }

    public static int ToInteger(this string strToConvert)
    {
        int strConvert;
        int.TryParse(strToConvert, out strConvert);
        return strConvert;
    }

    public static int ToInteger(this object objToConvert)
    {
        int objConvert;
        int.TryParse(objToConvert.ToString(), out objConvert);
        return objConvert;
    }

    public static string ToFormattedDate(this DateTime objToConvert, string cultureInfo = DefaultCultureInfo)
    {
        return objToConvert.ToString("dd/MM/yyyy", new CultureInfo(cultureInfo));
    }

    public static DateTime ToDateTime(this string objToConvert, string cultureInfo = DefaultCultureInfo)
    {
        DateTime obj;
        DateTime.TryParse(objToConvert, new CultureInfo(cultureInfo), out obj);
        return obj;
    }

    public static decimal ToDecimal(this string objToConvert, string cultureInfo = DefaultCultureInfo)
    {
        decimal obj;
        decimal.TryParse(objToConvert, new CultureInfo(cultureInfo), out obj);
        return obj;
    }
}