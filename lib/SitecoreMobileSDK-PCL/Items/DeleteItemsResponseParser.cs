namespace Sitecore.MobileSDK.Items
{
  using System;
  using System.Collections.Generic;
  using System.Threading;
  using Newtonsoft.Json;
  using Newtonsoft.Json.Linq;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.API.Items;

  public class DeleteItemsResponseParser
  {
    public static ScDeleteItemsResponse ParseResponse(string response, CancellationToken token)
    {
      //FIXME: @igk refactor

      if (response == "NoContent")
      {
        return new ScDeleteItemsResponse();
      }

      if (string.IsNullOrEmpty(response))
      {
        throw new ArgumentException("response shouldn't be empty or null");
      }

      token.ThrowIfCancellationRequested();

      JObject responseJObject = JObject.Parse(response);
      var statusCode = (int)responseJObject.SelectToken("statusCode");

      bool isSuccessfulCode = (200 <= statusCode) && (statusCode <= 299);

      if (!isSuccessfulCode)
      {
        var error = new SSCJsonStatusMessage(statusCode, ParseOrFail<string>(responseJObject, "$.error.message"));
        throw new SSCJsonErrorException(error);
      }

      var count = (int)responseJObject.SelectToken("$.result.count");

      var responseItems = responseJObject.SelectToken("$.result.itemIds");
      var itemsIds = new List<string>();

      foreach (JValue itemId in responseItems)
      {
        token.ThrowIfCancellationRequested();

        string itemIdString = itemId.Value<string>();
        itemsIds.Add(itemIdString);
      }

      if (itemsIds.Count != count)
      {
        throw new ParserException("[DELETE RESPONSE] Inconsistent items count in JSON response ");
      }

      return new ScDeleteItemsResponse(itemsIds);
    }

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