namespace Sitecore.MobileSDK.UserRequest.UploadMediaRequest
{
  using Sitecore.MobileSDK.Validators;

  public class UploadMediaItemByParentPathRequestBuilder : BaseUploadMediaRequestBuilder
  {
    public UploadMediaItemByParentPathRequestBuilder(string parentPath)
    {
      ItemPathValidator.ValidateMediaItemPath(parentPath, this.GetType().Name + ".ParentPath");
      this.mediaPath = parentPath;
    }
  }
}

