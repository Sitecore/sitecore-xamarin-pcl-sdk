namespace Sitecore.MobileSDK.UserRequest.ReadRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request.Parameters;


  public class ReadItemByPathRequestBuilder : AbstractGetVersionedItemRequestBuilder<IReadItemsByPathRequest>
  {
    public ReadItemByPathRequestBuilder(string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath, this.GetType().Name + ".ItemPath");

      this.itemPath = itemPath;
    }

    public override IReadItemsByPathRequest Build()
    {
      IPagingParameters pagingSettings = null;
      ISessionConfig sessionSettings = null;

      var result = new ReadItemByPathParameters(
        sessionSettings, 
        this.itemSourceAccumulator, 
        this.queryParameters, 
        pagingSettings,
        this.itemPath);

      return result;
    }

    private string itemPath;
  }
}

