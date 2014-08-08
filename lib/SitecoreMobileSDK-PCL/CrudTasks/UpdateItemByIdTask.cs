namespace Sitecore.MobileSDK.CrudTasks
{
  using Sitecore.MobileSDK.UrlBuilder.UpdateItem;
  using System.Net.Http;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;

  public class UpdateItemByIdTask : AbstractUpdateItemTask<IUpdateItemByIdRequest>
  {
    public UpdateItemByIdTask(UpdateItemByIdUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
      : base(httpClient, credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
    }

    protected override string UrlToGetItemWithRequest(IUpdateItemByIdRequest request)
    {
      return this.urlBuilder.GetUrlForRequest(request);
    }

    public override string GetFieldsListString(IUpdateItemByIdRequest request)
    {
      return this.urlBuilder.GetFieldValuesList(request);
    }

    private readonly UpdateItemByIdUrlBuilder urlBuilder;
  }
}

