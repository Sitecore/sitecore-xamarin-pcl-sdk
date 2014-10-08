
namespace Sitecore.MobileSDK.UrlBuilder.CreateItem
{
  using System;

  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.SessionSettings;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.API.MediaItem;

  public class UploadMediaUrlBuilder
  {
    public UploadMediaUrlBuilder(
      IRestServiceGrammar restGrammar,
      IWebApiUrlParameters webApiGrammar,
      ISessionConfig sessionConfig,
      IMediaLibrarySettings mediaSettings
    )
    {
      this.mediaSettings = mediaSettings;
      this.restGrammar = restGrammar;
      this.mediaSettings = mediaSettings;
      this.webApiGrammar = webApiGrammar;
    }
      
//    http://cms72u2.test24dk1.dk.sitecore.net/
//    -/item/v1/
//    sitecore/shell
//    %2Fsitecore%2Fmedia%20library%2Fwhitelabel%2Fbigimagetestdata?
//    name=TestMediaItem&
//      template=System%2FMedia%2FUnversioned%2FImage&
//      sc_database=web&
//      sc_lang=en&
//      payload=min

    public virtual string GetUrlForRequest(IMediaResourceUploadRequest request)
    {
      this.ValidateRequest(request);

      string hostUrl = this.GetHostUrlForRequest(request);
      string baseUrl = this.GetCommonPartForRequest(request).ToLowerInvariant();

      hostUrl = hostUrl + baseUrl;

      ItemSourceUrlBuilder sourceBuilder = new ItemSourceUrlBuilder(this.restGrammar, this.webApiGrammar, request.ItemSource);
      string itemSourceArgs = sourceBuilder.BuildUrlQueryString();

      string allParameters = itemSourceArgs;

      allParameters = allParameters + this.SpecificParametersForRequest(request);

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

    protected virtual void ValidateRequest(IMediaResourceUploadRequest request)
    {
      this.ValidateCommonRequest(request);
    }
  

    protected virtual string GetHostUrlForRequest(IMediaResourceUploadRequest request)
    {
      SessionConfigUrlBuilder sessionBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.webApiGrammar);
      string hostUrl = sessionBuilder.BuildUrlString(request.SessionSettings);

      return hostUrl;
    }

    private string GetCommonPartForRequest(IMediaResourceUploadRequest request)
    {
      return UrlBuilderUtils.EscapeDataString(this.mediaSettings.MediaLibraryRoot + request.CreateMediaParameters.MediaPath);
    }

    private string SpecificParametersForRequest(IMediaResourceUploadRequest request)
    {
      string result = "";

      if (null != request.CreateMediaParameters.FileName)
      {
        result += this.restGrammar.FieldSeparator
          + this.webApiGrammar.ItemNameParameterName
          + this.restGrammar.KeyValuePairSeparator
          + UrlBuilderUtils.EscapeDataString(request.CreateMediaParameters.ItemName);
      }

      if (null != request.CreateMediaParameters.ItemTemlate)
      {
        result += this.restGrammar.FieldSeparator
          + this.webApiGrammar.TemplateParameterName
          + this.restGrammar.KeyValuePairSeparator
          + UrlBuilderUtils.EscapeDataString(request.CreateMediaParameters.ItemTemlate);
      }

      return result;
    }

    private void ValidateCommonRequest(IMediaResourceUploadRequest request)
    {

    }

  

    private void Validate()
    {
      if (null == this.restGrammar)
      {
        throw new ArgumentNullException("[GetItemUrlBuilder] restGrammar cannot be null");
      }
      else if (null == this.webApiGrammar)
      {
        throw new ArgumentNullException("[GetItemUrlBuilder] webApiGrammar cannot be null");
      }
    }

    private IWebApiUrlParameters webApiGrammar;
    private IMediaLibrarySettings mediaSettings;
    private IRestServiceGrammar restGrammar;
    private ISessionConfig sessionConfig;
  }
}

