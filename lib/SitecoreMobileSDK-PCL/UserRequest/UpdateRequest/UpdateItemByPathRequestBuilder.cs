namespace Sitecore.MobileSDK.UrlBuilder.UpdateItem
{
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.API.Request;

  public class UpdateItemByPathRequestBuilder : AbstractUpdateItemRequestBuilder<IUpdateItemByPathRequest>
  {
    public UpdateItemByPathRequestBuilder(string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath, this.GetType().Name + ".itemPath");

      this.itemPath = itemPath;
    }

    public override IUpdateItemByPathRequest Build()
    {

      UpdateItemByPathParameters result = new UpdateItemByPathParameters(null, this.itemSourceAccumulator, this.queryParameters, this.FieldsRawValuesByName, this.itemPath);
      return result;
    }

    private readonly string itemPath;
  }
}
