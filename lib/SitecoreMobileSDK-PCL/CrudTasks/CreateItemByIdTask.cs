

namespace Sitecore.MobileSDK.CrudTasks
{
  using System;
  using System.Net.Http;

  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;

  public class CreateItemByIdTask : AbstractGetItemTask<ICreateItemByIdRequest>
  {
    public CreateItemByIdTask(CreateItemByIdUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor) 
      : base(httpClient, credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
    }

    protected override string UrlToGetItemWithRequest(ICreateItemByIdRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    private readonly CreateItemByIdUrlBuilder urlBuilder;
  }
}

