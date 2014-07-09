namespace Sitecore.MobileSDK.Authenticate
{
  using System;
  using System.Threading;
  using Newtonsoft.Json.Linq;
  using Sitecore.MobileSDK.Exceptions;

  public class AuthenticateResponseParser
  {
    public static WebApiJsonStatusMessage ParseResponse(string response, CancellationToken token)
    {
      if (string.IsNullOrEmpty(response))
      {
        throw new ArgumentException("response", "response shouldn't be empty or null");
      }

      token.ThrowIfCancellationRequested();

      JObject responseJObject = JObject.Parse(response);
      var statusCode = (int)responseJObject.SelectToken("statusCode");
      var message = (string)responseJObject.SelectToken("error.message");

      return new WebApiJsonStatusMessage(statusCode, message);
    }
  }
}