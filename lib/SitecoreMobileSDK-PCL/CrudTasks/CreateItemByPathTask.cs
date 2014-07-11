
namespace Sitecore.MobileSDK.CrudTasks
{
  using Sitecore.MobileSDK.UrlBuilder.CreateItem;
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;

  public class CreateItemByPathTask : AbstractCreateItemTask<ICreateItemByPathRequest>
  {
    public CreateItemByPathTask(CreateItemByPathUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor) 
      : base(httpClient, credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
    }

    protected override string UrlToGetItemWithRequest(ICreateItemByPathRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }
      
    public override string GetFieldsListString(ICreateItemByPathRequest request)
    {
      return this.urlBuilder.GetFieldValuesList(request);
    }

    private readonly CreateItemByPathUrlBuilder urlBuilder;
  }
}

