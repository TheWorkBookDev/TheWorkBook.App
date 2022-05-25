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
            string value = parameters[index];
            if (value.IndexOf(",") > -1)
            {
                string[] strArray = value.Split(new[] { ',' });
                foreach (string s in strArray)
                {
                    stringBuilder.Append(str + parameters.AllKeys[index] + "=" + s);
                    str = "&";
                }
            }
            else
            {
                stringBuilder.Append(str + parameters.AllKeys[index] + "=" + value);
                str = "&";
            }
        }
        return stringBuilder.ToString();
    }
}
