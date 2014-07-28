
namespace Sitecore.MobileSDK.UrlBuilder.MediaItem
{
  using System;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;


  using Sitecore.MobileSDK.Utils;
  using Sitecore.MobileSDK.SessionSettings;
  using System.Collections.Generic;

  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.Validators;


  public class MediaItemUrlBuilder
  {
    public MediaItemUrlBuilder(
      IRestServiceGrammar restGrammar, 
      ISessionConfig sessionConfig, 
      IItemSource itemSource)
    {
      this.itemSource = itemSource;
      this.restGrammar = restGrammar;
      this.sessionConfig = sessionConfig;

      this.Validate();
    }

    private void Validate()
    {
      if (null == this.itemSource)
      {
        throw new ArgumentNullException("[ResourceUrlBuilder.itemSource] Do not pass null");
      }
      else if (null == this.restGrammar)
      {
        throw new ArgumentNullException("[ResourceUrlBuilder.grammar] Do not pass null");
      }
      else if (null == this.sessionConfig)
      {
        throw new ArgumentNullException("[ResourceUrlBuilder.sessionConfig] Do not pass null");
      }
    }

    //    https://test.host/~/media/1.png.ashx?w=640&h=480
    public string BuildUrlStringForPath(string path, IDownloadMediaOptions options)
    {
      MediaPathValidator mediaPathValidator = new MediaPathValidator(this.sessionConfig);
      mediaPathValidator.ValidateMediaPath(path, this.GetType().Name + ".path");

      string relativePath = path;
      string result = SessionConfigValidator.AutocompleteInstanceUrl(this.sessionConfig.InstanceUrl);

      string lowerCasePathForComparisonNeeds = path.ToLowerInvariant();
      string lowerCaseMediaHook = this.sessionConfig.MediaPrefix.ToLowerInvariant();

      bool isMediaHookAvailable = lowerCasePathForComparisonNeeds.Contains(lowerCaseMediaHook);
      bool isExtensionAvailable = MediaPathValidator.IsItemPathHasExtension(path);

      string extensionWithDotPrefix = DOT_SEPARATOR + this.sessionConfig.DefaultMediaResourceExtension.ToLowerInvariant();

      if (isMediaHookAvailable)
      {
        result = result + this.restGrammar.PathComponentSeparator + Uri.EscapeUriString(relativePath);

        if ( !isExtensionAvailable )
        {
          result = result + extensionWithDotPrefix;
        }
      }
      else
      {
        result = result + this.restGrammar.PathComponentSeparator + lowerCaseMediaHook;

        string mediaLibraryRoot = this.sessionConfig.MediaLibraryRoot.ToLowerInvariant();

        int rootStartIndex = lowerCasePathForComparisonNeeds.IndexOf(mediaLibraryRoot);
        bool isMediaRootAvailable = (rootStartIndex >= 0);

        if ( isMediaRootAvailable )
        {
          relativePath = path.Remove(rootStartIndex, this.sessionConfig.MediaLibraryRoot.Length);
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

      if (this.itemSource.Version != null)
      {
        paramsString = this.AddOptionValueToPath(paramsString, MediaItemUrlBuilder.VERSION_KEY, this.itemSource.Version);
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
  }
}

