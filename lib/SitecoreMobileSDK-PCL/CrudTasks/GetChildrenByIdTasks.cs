namespace Sitecore.MobileSDK.CrudTasks
{
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Children;

  internal class GetChildrenByIdTasks : AbstractGetItemTask<IReadItemsByIdRequest>
  {
    public GetChildrenByIdTasks(ChildrenByIdUrlBuilder urlBuilder, HttpClient httpClient)
      : base(httpClient)
    {
      this.urlBuilder = urlBuilder;
    }

    protected override string UrlToGetItemWithRequest(IReadItemsByIdRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    private readonly ChildrenByIdUrlBuilder urlBuilder;
  }
}

