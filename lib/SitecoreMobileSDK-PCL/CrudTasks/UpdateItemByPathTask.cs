
namespace Sitecore.MobileSDK.CrudTasks
{
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;

  public class UpdateItemByPathTask : AbstractUpdateItemTask<IUpdateItemByPathRequest>
  {
    public UpdateItemByPathTask(UpdateItemByPathUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor) 
      : base(httpClient, credentialsHeadersCryptor)
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

