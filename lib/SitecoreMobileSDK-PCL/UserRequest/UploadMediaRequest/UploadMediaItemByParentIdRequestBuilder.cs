namespace Sitecore.MobileSDK.UserRequest.UploadMediaRequest
{
  using Sitecore.MobileSDK.Validators;

  public class UploadMediaItemByParentIdRequestBuilder : BaseUploadMediaRequestBuilder
  {
    public UploadMediaItemByParentIdRequestBuilder(string parentId)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(parentId, "UploadMediaItemByParentIdRequestBuilder.ParentId");
      ItemIdValidator.ValidateItemId (parentId, this.GetType().Name + ".ParentId");

      this.mediaPath = "";
      this.parentId = parentId;
    }
  }
}

