namespace Sitecore.MobileSDK.API.Request.Paging
{
  /// <summary>
  /// An interface to ensure "ItemsPerPage" parameter follows the "PageNumber" parameter.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface IPageNumberAccumulator<T> where T : class
  {
    /// <summary>
    /// Specifies an upper limit of items in a single response chunk.
    /// It should be a positive number.
    /// 
    /// The parameter is required by the "PageNumber" parameter. It is done to ensure that either both parameters are used ore none of them is specified.
    /// On a repeated invocation an InvalidOperationException is thrown.
    /// 
    /// </summary>
    /// <param name="itemsCountPerPage">Index of a page to download.
    /// An ArgumentException is thrown if negative number or zero is submitted.
    /// </param>
    /// <returns>
    /// An object capable of setting ReadItemRequest options. Usually it is same as "this" object. 
    /// </returns>
    IScopedRequestParametersBuilder<T> ItemsPerPage(int itemsCountPerPage);

    // @adk : returned by
    // IScopedRequestParametersBuilder<T> PageNumber(int pageNumber);
  }
}

