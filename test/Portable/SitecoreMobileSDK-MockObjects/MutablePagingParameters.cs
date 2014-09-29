namespace SitecoreMobileSDKMockObjects
{
  using System;
  using Sitecore.MobileSDK.API.Request.Parameters;



  public class MutablePagingParameters : IPagingParameters
  {
    public MutablePagingParameters(int pageNumber, int perPageCount)
    {
      this.PageNumber = pageNumber;
      this.ItemsPerPageCount = perPageCount;
    }
      
    public int ItemsPerPageCount { get; set; }
    public int PageNumber { get; set; }
  }
}

