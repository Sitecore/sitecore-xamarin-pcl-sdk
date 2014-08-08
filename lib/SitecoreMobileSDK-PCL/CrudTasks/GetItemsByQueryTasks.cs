namespace Sitecore.MobileSDK.CrudTasks
{
  using Sitecore.MobileSDK.Validators;
  using System.Net.Http;
  using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;
  using Sitecore.MobileSDK.PublicKey;
  using Sitecore.MobileSDK.API.Request;

  public class GetItemsByQueryTasks : AbstractGetItemTask<IReadItemsByQueryRequest>
  {
    public GetItemsByQueryTasks(ItemByQueryUrlBuilder urlBuilder, HttpClient httpClient, ICredentialsHeadersCryptor credentialsHeadersCryptor)
      : base(httpClient, credentialsHeadersCryptor)
    {
      this.urlBuilder = urlBuilder;
      this.Validate();
    }

    protected override string UrlToGetItemWithRequest(IReadItemsByQueryRequest request)
    {
      this.ValidateRequest(request);

      string result = this.urlBuilder.GetUrlForRequest(request);
      return result;
    }

    private void ValidateRequest(IReadItemsByQueryRequest request)
    {
      BaseValidator.CheckNullAndThrow(request, this.GetType().Name + ".request");
    }

    private void Validate()
    {
      BaseValidator.CheckNullAndThrow(this.urlBuilder, this.GetType().Name + ".urlBuilder");
    }

    private readonly ItemByQueryUrlBuilder urlBuilder;
  }
}

