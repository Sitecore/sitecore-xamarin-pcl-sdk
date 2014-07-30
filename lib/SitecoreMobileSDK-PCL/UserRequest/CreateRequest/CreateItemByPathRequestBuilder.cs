﻿
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
      if (string.IsNullOrEmpty(this.itemParametersAccumulator.ItemName))
      {
        BaseValidator.ThrowNullOrEmptyParameterException(this.GetType().Name + ".ItemName");
      }

      if (string.IsNullOrEmpty(this.itemParametersAccumulator.ItemTemplate))
      {
        BaseValidator.ThrowNullOrEmptyParameterException(this.GetType().Name + ".ItemTemplate");
      }

      CreateItemByPathParameters result = 
        new CreateItemByPathParameters(null, this.itemSourceAccumulator, this.queryParameters, this.itemParametersAccumulator, this.ItemPath);
      return result;
    }

    private readonly string ItemPath;
  }
}

