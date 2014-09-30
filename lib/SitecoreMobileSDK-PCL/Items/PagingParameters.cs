namespace Sitecore.MobileSDK.Items
{
  using System;
  using Sitecore.MobileSDK.API.Request.Parameters;


  internal class PagingParameters : IPagingParameters
  {
    public PagingParameters(int? optionalItemsPerPage, int? optionalPageNumber)
    {
      this.OptionalItemsPerPage = optionalItemsPerPage;
      this.OptionalPageNumber = optionalPageNumber;
    }

    public IPagingParameters PagingParametersCopy()
    {
      return new PagingParameters(this.OptionalItemsPerPage, this.OptionalPageNumber);
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

    #region Optional
    public int? OptionalItemsPerPage { get; private set; }
    public int? OptionalPageNumber { get; private set; }
    #endregion
  }
}

