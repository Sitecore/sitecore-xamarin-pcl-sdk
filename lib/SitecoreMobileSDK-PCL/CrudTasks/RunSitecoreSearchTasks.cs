
namespace Sitecore.MobileSDK.CrudTasks
{
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.Search;

  internal class RunSitecoreSearchTasks : AbstractGetItemTask<ISitecoreSearchRequest>
  {
    public RunSitecoreSearchTasks(RunSitecoreSearchUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
      : base(httpClient, credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
    }

    protected override string UrlToGetItemWithRequest(ISitecoreSearchRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    private readonly RunSitecoreSearchUrlBuilder urlBuilder;
  }
}

