namespace Sitecore.MobileSDK.UserRequest.ReadRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class ReadItemByPathRequestBuilder : AbstractGetVersionedItemRequestBuilder<IReadItemsByPathRequest>
  {
    public ReadItemByPathRequestBuilder(string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath, this.GetType().Name + ".ItemPath");

      this.itemPath = itemPath;
    }

    public override IReadItemsByPathRequest Build()
    {
      var result = new ReadItemByPathParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemPath);
      return result;
    }

    private string itemPath;
  }
}

