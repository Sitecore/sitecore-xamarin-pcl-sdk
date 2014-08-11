namespace Sitecore.MobileSDK.API.Request.Template
{
  public interface ISetTemplateBuilder<T> where T : class
  {
    ISetNewItemNameBuilder<T> ItemTemplatePath(string templatePath);
    ISetNewItemNameBuilder<T> ItemTemplateId(string templateId);
//    ICreateItemRequestParametersBuilder<T> BranchId(string branchId);
  }
}

