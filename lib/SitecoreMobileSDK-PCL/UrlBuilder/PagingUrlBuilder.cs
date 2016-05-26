namespace Sitecore.MobileSDK
{
  using System;
  using System.Globalization;

  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.API.Request.Parameters;


  internal class PagingUrlBuilder
  {
    private IRestServiceGrammar restGrammar;
    private  ISSCUrlParameters sscGrammar;

    public PagingUrlBuilder(IRestServiceGrammar restGrammar, ISSCUrlParameters sscGrammar)
    {
      this.restGrammar = restGrammar;
      this.sscGrammar = sscGrammar;
    }

    public string BuildUrlQueryString(IPagingParameters pagingParameters)
    {
      if (null == pagingParameters)
      {
        return null;
      }
        
      var invariangCulture = CultureInfo.InvariantCulture;
      string strPerPage = pagingParameters.ItemsPerPageCount.ToString(invariangCulture);
      string strPageNumber = pagingParameters.PageNumber.ToString(invariangCulture);

      string result = 
        this.sscGrammar.PageNumberParameterName + this.restGrammar.KeyValuePairSeparator + strPageNumber +
        this.restGrammar.FieldSeparator +
        this.sscGrammar.ItemsPerPageParameterName + this.restGrammar.KeyValuePairSeparator + strPerPage;

      return result;
    }
  }
}

