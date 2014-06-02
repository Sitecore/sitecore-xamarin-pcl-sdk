
namespace Sitecore.MobileSDK.Utils
{
    using System;

    internal class UrlBuilderUtils
    {
        public static string EscapeDataString(string originalString)
        {
            string result = Uri.EscapeDataString(originalString);

            result = result.Replace("(", "%28");
            result = result.Replace(")", "%29");

            return result;
        }
    }
}
