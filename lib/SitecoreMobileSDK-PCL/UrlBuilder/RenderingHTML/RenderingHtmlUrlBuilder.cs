using Sitecore.MobileSDK.SessionSettings;

namespace Sitecore.MobileSDK.UrlBuilder.RenderingHtml
{
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.Validators;

  public class RenderingHtmlUrlBuilder
  {
    private IRestServiceGrammar restGrammar;
    private IWebApiUrlParameters webApiGrammar;

    public RenderingHtmlUrlBuilder(IRestServiceGrammar restGrammar, IWebApiUrlParameters webApiGrammar)
    {
      this.restGrammar = restGrammar;
      this.webApiGrammar = webApiGrammar;
    }

    public string GetUrlForRequest(IGetRenderingHtmlRequest request)
    {
      this.ValidateSpecificRequest(request);

      string hostUrl = this.GetHostUrlForRequest(request)
            + this.restGrammar.PathComponentSeparator
            + this.webApiGrammar.ItemWebApiActionsEndpoint
            + this.webApiGrammar.ItemWebApiGetRenderingAction;

      string baseParameters = this.GetCommonPartForRequest(request).ToLowerInvariant();
      string specificParameters = this.GetSpecificPartForRequest(request);

      string allParameters = baseParameters;

      if (!string.IsNullOrEmpty(specificParameters))
      {
        allParameters =
          allParameters +
          this.restGrammar.FieldSeparator + specificParameters;
      }

      bool shouldTruncateBaseUrl = (!string.IsNullOrEmpty(allParameters) && allParameters.StartsWith(this.restGrammar.FieldSeparator));
      while (shouldTruncateBaseUrl)
      {
        allParameters = allParameters.Substring(1);
        shouldTruncateBaseUrl = (!string.IsNullOrEmpty(allParameters) && allParameters.StartsWith(this.restGrammar.FieldSeparator));
      }


      string result = hostUrl;

      if (!string.IsNullOrEmpty(allParameters))
      {
        result =
          result +
          this.restGrammar.HostAndArgsSeparator + allParameters;
      }

      return result;
    }

    private string GetSpecificPartForRequest(IGetRenderingHtmlRequest request)
    {
      string escapedSourceId = UrlBuilderUtils.EscapeDataString(request.SourceId);
      string escapedRenderingId = UrlBuilderUtils.EscapeDataString(request.RenderingId);
      string result = this.webApiGrammar.ItemIdParameterName + this.restGrammar.KeyValuePairSeparator + escapedSourceId
        + restGrammar.FieldSeparator 
        + this.webApiGrammar.RenderingIdParameterName + this.restGrammar.KeyValuePairSeparator + escapedRenderingId;

      result = result.ToLowerInvariant();

      //must be case sensitive
      result += this.GetCustomParametersString (request);

      return result;
    }

    private string GetCustomParametersString(IGetRenderingHtmlRequest request)
    {
      string result = "";
      if (null != request.ParametersValuesByName)
      {
        foreach (var param in request.ParametersValuesByName)
        {
          string escapedParamName = UrlBuilderUtils.EscapeDataString (param.Key);
          string escapedParamValue = UrlBuilderUtils.EscapeDataString (param.Value);
          result += 
          this.restGrammar.FieldSeparator +
          escapedParamName +
          this.restGrammar.KeyValuePairSeparator +
          escapedParamValue;
        }
      }
      return result;
    }

    private string GetCommonPartForRequest(IGetRenderingHtmlRequest request)
    {
      ItemSourceUrlBuilder sourceBuilder = new ItemSourceUrlBuilder(this.restGrammar, this.webApiGrammar, request.ItemSource);
      string itemSourceArgs = sourceBuilder.BuildUrlQueryString();

      string result = string.Empty;

      string[] restSubQueries = { itemSourceArgs };
      foreach (string currentSubQuery in restSubQueries)
      {
        if (!string.IsNullOrEmpty(currentSubQuery))
        {
          result =
            result +
            this.restGrammar.FieldSeparator + currentSubQuery;
        }
      }

      return result.ToLowerInvariant();
    }

    protected void ValidateSpecificRequest(IGetRenderingHtmlRequest request)
    {
      ItemIdValidator.ValidateItemId(request.SourceId, this.GetType().Name + ".SourceId");
      ItemIdValidator.ValidateItemId(request.RenderingId, this.GetType().Name + ".RenderingId");
    }

    protected virtual string GetHostUrlForRequest(IGetRenderingHtmlRequest request)
    {
      SessionConfigUrlBuilder sessionBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
      string hostUrl = sessionBuilder.BuildUrlString(request.SessionSettings);

      return hostUrl;
    }
  }
}
