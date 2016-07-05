using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Sitecore.MobileSDK.CrudTasks
{
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;

  internal class UpdateItemByIdTask : AbstractUpdateItemTask<IUpdateItemByIdRequest>
  {
    public UpdateItemByIdTask(UpdateItemByIdUrlBuilder urlBuilder, HttpClient httpClient)
      : base(httpClient)
    {
      this.urlBuilder = urlBuilder;
    }

    protected override string UrlToGetItemWithRequest(IUpdateItemByIdRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    public override string GetFieldsListString(IUpdateItemByIdRequest request)
    {
      string result = string.Empty;

      JObject jsonObject = new JObject();

      bool fieldsAvailable = (null != request.FieldsRawValuesByName);
      if (fieldsAvailable)
      {
        fieldsAvailable = (request.FieldsRawValuesByName.Count > 0);
      }

      //TODO: IGK refactor this

      if (fieldsAvailable)
      {
        foreach (var fieldElem in request.FieldsRawValuesByName)
        {
          jsonObject.Add(fieldElem.Key, fieldElem.Value);
        }
      }

      result = jsonObject.ToString(Formatting.None);

      return result;
    }
    private readonly UpdateItemByIdUrlBuilder urlBuilder;
  }
}

