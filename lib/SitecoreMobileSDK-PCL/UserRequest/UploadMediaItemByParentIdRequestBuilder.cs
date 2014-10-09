
namespace Sitecore.MobileSDK.UserRequest
{
  using System.IO;
  using Sitecore.MobileSDK.Validators;

  public class UploadMediaItemByParentIdRequestBuilder : UploadMediaItemByParentPathRequestBuilder
  {
    public UploadMediaItemByParentIdRequestBuilder(string parentId)
      : base(null)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow (parentId, "UploadMediaItemRequestParametersBuilder.parentId is required");
      ItemIdValidator.ValidateItemId (parentId, this.GetType().Name + ".parentId");
      this.parentId = parentId;
    }
  }
}

