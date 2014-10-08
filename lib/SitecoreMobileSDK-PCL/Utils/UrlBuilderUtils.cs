namespace Sitecore.MobileSDK.Utils
{
  using System;

  internal class UrlBuilderUtils
  {
    public static string EscapeDataString(string originalString)
    {
      if (string.IsNullOrEmpty(originalString))
      {
        return originalString;
      }

      string result = Uri.EscapeDataString(originalString);

      result = result.Replace("=", "%3d");
      result = result.Replace("%3D", "%3d");

      result = result.Replace("[", "%5b");
      result = result.Replace("%5B", "%5b");

      result = result.Replace("]", "%5d");
      result = result.Replace("%5D", "%5d");


      result = result.Replace("(", "%28");
      result = result.Replace(")", "%29");

      result = result.Replace(":", "%3a");
      result = result.Replace("%3A", "%3a");

      result = result.Replace("*", "%2a");
      result = result.Replace("%2A", "%2a");

      result = result.Replace("/", "%2f");
      result = result.Replace("%2F", "%2f");

      result = result.Replace("'", "%27");
      result = result.Replace("!", "%21");

      // result = result.Replace("?", "%3f");
      result = result.Replace("%3F", "%3f");

      return result;
    }
  }
}
