namespace Sitecore.MobileSDK.CrudTasks.Resource
{
  using System;
  using System.Threading;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;
  using Sitecore.MobileSDK.API.Exceptions;

  public static class HashedMediaUrlParser
  {
    public static string Parse(string responseString, CancellationToken cancelToken)
    {
      if (string.IsNullOrEmpty(responseString))
      {
        throw new ArgumentException("response cannot null or empty");
      }

      cancelToken.ThrowIfCancellationRequested();
      JObject response = JObject.Parse(responseString);

      cancelToken.ThrowIfCancellationRequested();
      int statusCode = ParseOrFail<int>(response, "$.statusCode");
      bool isSuccessfulCode = (200 <= statusCode) && (statusCode <= 299);

      if (!isSuccessfulCode)
      {
        var error = new SSCJsonStatusMessage(statusCode, ParseOrFail<string>(response, "$.error.message"));
        throw new SSCJsonErrorException(error);
      }

      cancelToken.ThrowIfCancellationRequested();
      return ParseOrFail<string>(response, "$.result");
    }

    // copypaste
    private static T ParseOrFail<T>(JObject json, string path)
    {
      JToken objectToken = json.SelectToken(path);
      if (null == objectToken)
      {
        throw new JsonException("Invalid json");
      }

      return objectToken.Value<T>();
    }
  }
}

