namespace Sitecore.MobileSDK.API.Request.Template
{
  public interface ISetNewItemNameBuilder<T> where T : class
  {
    ICreateItemRequestParametersBuilder<T> ItemName(string template);
  }
}

