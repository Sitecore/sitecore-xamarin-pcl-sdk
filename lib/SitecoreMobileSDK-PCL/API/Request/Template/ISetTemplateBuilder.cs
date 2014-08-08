namespace Sitecore.MobileSDK.API.Request.Template
{
  public interface ISetTemplateBuilder<T> where T : class
  {
    ICreateItemRequestParametersBuilder<T> ItemTemplatePath(string template);
    ICreateItemRequestParametersBuilder<T> ItemTemplateId(string template);
  }
}

