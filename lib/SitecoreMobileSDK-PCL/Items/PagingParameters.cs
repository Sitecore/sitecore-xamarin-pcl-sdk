namespace Sitecore.MobileSDK.Items
{
  using System;
  using Sitecore.MobileSDK.API.Request.Parameters;


  internal class PagingParameters : IPagingParameters
  {
    public int? OptionalItemsPerPage { get; private set; }
    public int? OptionalPageNumber { get; private set; }

    public PagingParameters(int? optionalItemsPerPage, int? optionalPageNumber)
    {
      this.OptionalItemsPerPage = optionalItemsPerPage;
      this.OptionalPageNumber = optionalPageNumber;
    }

    #region IPagingParameters
    public int ItemsPerPageCount 
    { 
      get
      {
        return this.OptionalItemsPerPage.Value;
      }
    }

    public int PageNumber 
    { 
      get
      {
        return this.OptionalPageNumber.Value;
      }
    }
    #endregion


  }
}

