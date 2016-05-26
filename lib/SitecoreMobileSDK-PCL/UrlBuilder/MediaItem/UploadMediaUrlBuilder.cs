
namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
  using System;

  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.Validators;
  using Sitecore.MobileSDK.SessionSettings;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;
  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.API.MediaItem;

  public class UploadMediaUrlBuilder
  {
    public UploadMediaUrlBuilder(
      IRestServiceGrammar restGrammar,
      ISSCUrlParameters sscGrammar,
      ISessionConfig sessionConfig,
      IMediaLibrarySettings mediaSettings
    )
    {
      this.mediaSettings = mediaSettings;
      this.restGrammar = restGrammar;
      this.mediaSettings = mediaSettings;
      this.sscGrammar = sscGrammar;
    }

    public virtual string GetUrlForRequest(IMediaResourceUploadRequest request)
    {

      string hostUrl = this.GetHostUrlForRequest(request);
      string baseUrl = this.GetCommonPartForRequest(request).ToLowerInvariant();

      hostUrl = hostUrl + baseUrl;

      ItemSourceUrlBuilder sourceBuilder = new ItemSourceUrlBuilder(this.restGrammar, this.sscGrammar, request.ItemSource);
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

    protected virtual string GetHostUrlForRequest(IMediaResourceUploadRequest request)
    {
      SessionConfigUrlBuilder sessionBuilder = new SessionConfigUrlBuilder(this.restGrammar, this.sscGrammar);
      string hostUrl = sessionBuilder.BuildUrlString(request.SessionSettings);

      return hostUrl;
    }

    private string GetCommonPartForRequest(IMediaResourceUploadRequest request)
    {
      return UrlBuilderUtils.EscapeDataString(this.mediaSettings.MediaLibraryRoot + request.UploadOptions.MediaPath);
    }

    private string SpecificParametersForRequest(IMediaResourceUploadRequest request)
    {
      string result = "";

      if (null != request.UploadOptions.FileName)
      {
        result += this.restGrammar.FieldSeparator
          + this.sscGrammar.ItemNameParameterName
          + this.restGrammar.KeyValuePairSeparator
          + UrlBuilderUtils.EscapeDataString(request.UploadOptions.ItemName);
      }

      if (null != request.UploadOptions.ItemTemplatePath)
      {
        result += this.restGrammar.FieldSeparator
          + this.sscGrammar.TemplateParameterName
          + this.restGrammar.KeyValuePairSeparator
          + UrlBuilderUtils.EscapeDataString(request.UploadOptions.ItemTemplatePath);
      }

      if (null != request.UploadOptions.ParentId)
      {
        result += this.restGrammar.FieldSeparator
          + this.sscGrammar.ItemIdParameterName
          + this.restGrammar.KeyValuePairSeparator
          + UrlBuilderUtils.EscapeDataString(request.UploadOptions.ParentId);
      }

      return result;
    }

    private void Validate()
    {
      if (null == this.restGrammar)
      {
        throw new ArgumentNullException("[GetItemUrlBuilder] restGrammar cannot be null");
      }
      else if (null == this.sscGrammar)
      {
        throw new ArgumentNullException("[GetItemUrlBuilder] sscGrammar cannot be null");
      }
    }

    private ISSCUrlParameters sscGrammar;
    private IMediaLibrarySettings mediaSettings;
    private IRestServiceGrammar restGrammar;
    private ISessionConfig sessionConfig;
  }
}

