using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Sitecore.MobileSDK.CrudTasks
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;

  internal class CreateItemByPathTask : AbstractCreateItemTask<ICreateItemByPathRequest>
  {
    public CreateItemByPathTask(CreateItemByPathUrlBuilder urlBuilder, HttpClient httpClient)
      : base(httpClient)
    {
      this.urlBuilder = urlBuilder;
    }

    protected override string UrlToGetItemWithRequest(ICreateItemByPathRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    public override string GetFieldsListString(ICreateItemByPathRequest request)
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

      //TODO: IGK check do we need some fields more. Documentation have no such content.
      jsonObject.Add("TemplateID", request.ItemTemplateId);
      jsonObject.Add("ItemName", request.ItemName);

      result = jsonObject.ToString(Formatting.None);

      return result;
    }

    private readonly CreateItemByPathUrlBuilder urlBuilder;
  }
}

