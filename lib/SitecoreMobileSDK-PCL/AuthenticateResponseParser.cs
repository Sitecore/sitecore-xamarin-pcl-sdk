namespace UnitTest_Desktop_NUnitLite
{
  using System;
  using System.Threading;
  using Newtonsoft.Json.Linq;

  public class AuthenticateResponseParser
  {
    public static Boolean ParseResponse(string response, CancellationToken token)
    {
      if (string.IsNullOrEmpty(response))
      {
        throw new ArgumentException("response", "response shouldn't be empty or null");
      }

      JObject responseJObject = JObject.Parse(response);
      int statusCode = (int)responseJObject.SelectToken("statusCode");
      
      return statusCode == 200;
    }
  }
}