namespace Sitecore.MobileSDK.CrudTasks
{
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;

  internal class UpdateItemByPathTask : AbstractUpdateItemTask<IUpdateItemByPathRequest>
  {
    public UpdateItemByPathTask(UpdateItemByPathUrlBuilder urlBuilder, HttpClient httpClient)
      : base(httpClient)
    {
      this.urlBuilder = urlBuilder;
    }

    protected override string UrlToGetItemWithRequest(IUpdateItemByPathRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    public override string GetFieldsListString(IUpdateItemByPathRequest request)
    {
      return this.urlBuilder.GetFieldValuesList(request);
    }

    private readonly UpdateItemByPathUrlBuilder urlBuilder;
  }
}

