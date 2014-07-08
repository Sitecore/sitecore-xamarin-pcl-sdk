using System;

namespace Sitecore.MobileSDK
{
  public interface ICreateItemRequestParametersBuilder<T> : IGetItemRequestParametersBuilder<T>
    where T : class
  {
    ICreateItemRequestParametersBuilder<T> ItemTemplate(string template);
  }
}

