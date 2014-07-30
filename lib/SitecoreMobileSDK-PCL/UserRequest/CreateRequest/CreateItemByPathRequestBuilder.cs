
namespace Sitecore.MobileSDK.UserRequest.CreateRequest
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.Validators;

  public class CreateItemByPathRequestBuilder : AbstractCreateItemRequestBuilder<ICreateItemByPathRequest>
  {
    public CreateItemByPathRequestBuilder(string itemPath)
    {
      ItemPathValidator.ValidateItemPath(itemPath, this.GetType().Name + ".itemPath");
      this.ItemPath = itemPath;
    }

    public override ICreateItemByPathRequest Build()
    {
      BaseValidator.CheckForNullAndEmptyOrThrow(this.itemParametersAccumulator.ItemName,
        this.GetType().Name + ".ItemName");

      BaseValidator.CheckForNullAndEmptyOrThrow(this.itemParametersAccumulator.ItemTemplate,
        this.GetType().Name + ".ItemTemplate");

      CreateItemByPathParameters result =
        new CreateItemByPathParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemParametersAccumulator, this.ItemPath);
      return result;
    }

    private readonly string ItemPath;
  }
}

