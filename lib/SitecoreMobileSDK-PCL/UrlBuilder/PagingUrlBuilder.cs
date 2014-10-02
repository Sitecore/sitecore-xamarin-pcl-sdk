namespace Sitecore.MobileSDK
{
  using System;
  using System.Globalization;

  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.API.Request.Parameters;


  internal class PagingUrlBuilder
  {
    private IRestServiceGrammar restGrammar;
    private  IWebApiUrlParameters webApiGrammar;

    public PagingUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
    {
      this.restGrammar = restGrammar;
      this.webApiGrammar = webApiGrammar;
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
        this.webApiGrammar.PageNumberParameterName + this.restGrammar.KeyValuePairSeparator + strPageNumber +
        this.restGrammar.FieldSeparator +
        this.webApiGrammar.ItemsPerPageParameterName + this.restGrammar.KeyValuePairSeparator + strPerPage;

      return result;
    }
  }
}

