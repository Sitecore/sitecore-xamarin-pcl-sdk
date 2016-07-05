namespace Sitecore.MobileSDK.CrudTasks
{
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.ItemByPath;


  internal class GetItemsByPathTasks : AbstractGetItemTask<IReadItemsByPathRequest>
  {
    public GetItemsByPathTasks(ItemByPathUrlBuilder urlBuilder, HttpClient httpClient)
      : base(httpClient)
    {
      this.urlBuilder = urlBuilder;
    }
    protected override string UrlToGetItemWithRequest(IReadItemsByPathRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    private readonly ItemByPathUrlBuilder urlBuilder;
  }
}
