
namespace Sitecore.MobileSDK.CrudTasks
{
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Search;

  internal class RunStoredSearchTasks : AbstractGetItemTask<ISitecoreStoredSearchRequest>
  {
    public RunStoredSearchTasks(RunStoredSearchUrlBuilder urlBuilder, HttpClient httpClient)
      : base(httpClient)
    {
      this.urlBuilder = urlBuilder;
    }

    protected override string UrlToGetItemWithRequest(ISitecoreStoredSearchRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    private readonly RunStoredSearchUrlBuilder urlBuilder;
  }
}

