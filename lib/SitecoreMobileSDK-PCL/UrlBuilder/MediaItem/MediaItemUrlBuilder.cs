namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
  using System;
  using System.Globalization;
  using Sitecore.MobileSDK.API.MediaItem;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.Validators;

  public class MediaItemUrlBuilder
  {
    public MediaItemUrlBuilder(
      IRestServiceGrammar restGrammar,
      ISessionConfig sessionConfig,
      IMediaLibrarySettings mediaSettings,
      IItemSource itemSource)
    {
      this.itemSource = itemSource;
      this.restGrammar = restGrammar;
      this.mediaSettings = mediaSettings;
      this.sessionConfig = sessionConfig;

      this.Validate();
    }

    private void Validate()
    {
      BaseValidator.CheckNullAndThrow(this.itemSource, this.GetType().Name + ".itemSource");

      BaseValidator.CheckNullAndThrow(this.restGrammar, this.GetType().Name + ".restGrammar");

      BaseValidator.CheckNullAndThrow(this.sessionConfig, this.GetType().Name + ".sessionConfig");
    }

    //    https://test.host/~/media/1.png.ashx?w=640&h=480
    public string BuildUrlStringForPath(string path, IDownloadMediaOptions options)
    {
      MediaPathValidator mediaPathValidator = new MediaPathValidator(this.mediaSettings);
      mediaPathValidator.ValidateMediaPath(path, this.GetType().Name + ".Path");

      string relativePath = path;
      string result = SessionConfigValidator.AutocompleteInstanceUrl(this.sessionConfig.InstanceUrl);

      string lowerCasePathForComparisonNeeds = path.ToLowerInvariant();
      string lowerCaseMediaHook = this.mediaSettings.MediaPrefix.ToLowerInvariant();

      bool isMediaHookAvailable = lowerCasePathForComparisonNeeds.Contains(lowerCaseMediaHook);
      bool isExtensionAvailable = MediaPathValidator.IsItemPathHasExtension(path);

      string extensionWithDotPrefix = DOT_SEPARATOR + this.mediaSettings.DefaultMediaResourceExtension.ToLowerInvariant();

      if (isMediaHookAvailable)
      {
        result = result + this.restGrammar.PathComponentSeparator + Uri.EscapeUriString(relativePath);

        if (!isExtensionAvailable)
        {
          result = result + extensionWithDotPrefix;
        }
      }
      else
      {
        result = result + this.restGrammar.PathComponentSeparator + lowerCaseMediaHook;

        string mediaLibraryRoot = this.mediaSettings.MediaLibraryRoot.ToLowerInvariant();

        int rootStartIndex = lowerCasePathForComparisonNeeds.IndexOf(mediaLibraryRoot);
        bool isMediaRootAvailable = (rootStartIndex >= 0);

        if (isMediaRootAvailable)
        {
          relativePath = path.Remove(rootStartIndex, this.mediaSettings.MediaLibraryRoot.Length);
        }


        bool isInvalidRelativePath = string.IsNullOrEmpty(relativePath);
        if (isInvalidRelativePath)
        {
          throw new ArgumentException("ResourceUrlBuilder.BuildUrlStringForPath invalid path");
        }

        relativePath = Uri.EscapeUriString(relativePath);

        result = result + relativePath + extensionWithDotPrefix;
      }

      result = this.AppendUrlStringWithDownloadOptions(result, options);
      return result.ToLowerInvariant();
    }

    private string SerializeOptions(IDownloadMediaOptions options)
    {
      bool isValidMediaOptions = MediaOptionsValidator.IsValidMediaOptions(options);
      if (!isValidMediaOptions)
      {
        return string.Empty;
      }

      Dictionary<string, string> optionsDictionary = options.OptionsDictionary;
      string paramsString = string.Empty;
      foreach (var pair in optionsDictionary)
      {
        paramsString = this.AddOptionValueToPath(paramsString, pair.Key, pair.Value);
      }

      return paramsString;
    }

    private string AppendUrlStringWithDownloadOptions(string path, IDownloadMediaOptions options)
    {
      string paramsString = this.SerializeOptions(options);

      if (this.itemSource.Database != null)
      {
        paramsString = this.AddOptionValueToPath(paramsString, MediaItemUrlBuilder.DATABASE_KEY, this.itemSource.Database);
      }

      if (this.itemSource.Language != null)
      {
        paramsString = this.AddOptionValueToPath(paramsString, MediaItemUrlBuilder.LANGUAGE_KEY, this.itemSource.Language);
      }

      if (null != this.itemSource.VersionNumber)
      {
        string strVersionNumber = this.itemSource.VersionNumber.Value.ToString(CultureInfo.InvariantCulture);
        paramsString = this.AddOptionValueToPath(paramsString, MediaItemUrlBuilder.VERSION_KEY, strVersionNumber);
      }

      if (!String.IsNullOrEmpty(paramsString))
      {
        paramsString = paramsString.Remove(paramsString.Length - this.restGrammar.FieldSeparator.Length);
        path += this.restGrammar.HostAndArgsSeparator;
        path += paramsString;
      }

      return path;
    }

    private string AddOptionValueToPath(string path, string key, string value)
    {
      return path += key + this.restGrammar.KeyValuePairSeparator + value + this.restGrammar.FieldSeparator;
    }


    private const string DOT_SEPARATOR = ".";
    private const string LANGUAGE_KEY = "la";
    private const string VERSION_KEY = "vs";
    private const string DATABASE_KEY = "db";

    private IItemSource itemSource;
    private IRestServiceGrammar restGrammar;
    private ISessionConfig sessionConfig;
    private IMediaLibrarySettings mediaSettings;
  }
}

