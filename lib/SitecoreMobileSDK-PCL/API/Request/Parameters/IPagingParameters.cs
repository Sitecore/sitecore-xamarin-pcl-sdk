namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System;

  /// <summary>
  /// An interface containing paging options. These options split large responses to smaller chunks.
  /// Using them can make your UI more responsive.
  ///  </summary>
  public interface IPagingParameters
  {
    IPagingParameters PagingParametersCopy();

    /// <summary>
    /// An upper limit of items amount for the response.
    /// </summary>
    /// <returns>A positive number</returns>
    int ItemsPerPageCount { get; }


    /// <summary>
    /// The number of a page to load.
    /// </summary>
    /// <returns>A positive number</returns>
    int PageNumber { get; }
  }
}

