using System.Collections.Specialized;
using System.Text;

namespace TheWorkBook.Extensions;
public static class UriExtensions
{
    public static Uri AttachParameters(this Uri uri, NameValueCollection parameters)
    {
        string parameterString = GetParametersString(parameters);
        return new Uri(uri + parameterString);
    }

    public static string AttachParameters(this string uri, NameValueCollection parameters)
    {
        string parameterString = GetParametersString(parameters);
        return uri + parameterString;
    }

    private static string GetParametersString(NameValueCollection parameters)
    {
        var stringBuilder = new StringBuilder();
        string str = "?";
        for (int index = 0; index < parameters.Count; ++index)
        {
            stringBuilder.Append(str + parameters.AllKeys[index] + "=" + parameters[index]);
            str = "&";
        }
        return stringBuilder.ToString();
    }
}

