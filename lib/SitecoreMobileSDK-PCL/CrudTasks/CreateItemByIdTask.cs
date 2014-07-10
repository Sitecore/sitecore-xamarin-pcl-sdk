
namespace Sitecore.MobileSDK.CrudTasks
{
  using System;
  using System.Net.Http;
  using System.Net.Http.Headers;
  using System.Threading.Tasks;
  using System.Threading;
  using System.Diagnostics;

  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;

  public class CreateItemByIdTask : AbstractCreateItemTask<ICreateItemByIdRequest>
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
      
    public override string GetFieldsListString(ICreateItemByIdRequest request)
    {
      return this.urlBuilder.GetFieldValuesList(request);
    }

    private readonly CreateItemByIdUrlBuilder urlBuilder;
  }
}

