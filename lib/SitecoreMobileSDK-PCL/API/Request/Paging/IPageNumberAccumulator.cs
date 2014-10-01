namespace Sitecore.MobileSDK.API.Request.Paging
{
  using System;


  public interface IPageNumberAccumulator<T> where T : class
  {
    IScopedRequestParametersBuilder<T> ItemsPerPage(int itemsCountPerPage);

    // @adk : returned by
    // IScopedRequestParametersBuilder<T> PageNumber(int pageNumber);
  }
}

