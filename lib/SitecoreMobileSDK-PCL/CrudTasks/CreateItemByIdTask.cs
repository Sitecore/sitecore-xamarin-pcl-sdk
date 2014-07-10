
namespace Sitecore.MobileSDK.CrudTasks
{
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;

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

