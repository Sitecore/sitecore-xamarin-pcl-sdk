namespace MobileSDKUnitTest.Mock
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

    public IPagingParameters PagingParametersCopy()
    {
      return new MutablePagingParameters(this.PageNumber, this.ItemsPerPageCount);
    }
      
    public int ItemsPerPageCount { get; set; }
    public int PageNumber { get; set; }
  }
}

