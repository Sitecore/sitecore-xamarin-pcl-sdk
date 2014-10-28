namespace Sitecore.MobileSDK.UserRequest.UpdateRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Validators;

  public class UpdateItemByPathRequestBuilder : AbstractUpdateItemRequestBuilder<IUpdateItemByPathRequest>
  {
    public UpdateItemByPathRequestBuilder(string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath, this.GetType().Name + ".ItemPath");

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
