namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;

  public interface ISearchItemRequestParametersBuilder<T> : IGetVersionedItemRequestParametersBuilder<T>
    where T : class
  {
    ISearchItemRequestParametersBuilder<T> AddAscendingFieldsToSort(params string[] fieldParams);
    ISearchItemRequestParametersBuilder<T> AddDescendingFieldsToSort(params string[] fieldParams);
    ISearchItemRequestParametersBuilder<T> AddAscendingFieldsToSort(IEnumerable<string> fields);
    ISearchItemRequestParametersBuilder<T> AddDescendingFieldsToSort(IEnumerable<string> fields);
  }
}

