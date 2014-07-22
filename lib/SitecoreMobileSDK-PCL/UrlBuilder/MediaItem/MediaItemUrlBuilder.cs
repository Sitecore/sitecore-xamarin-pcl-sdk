
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

    public MediaItemUrlBuilder(IRestServiceGrammar restGrammar, ISessionConfig sessionConfig, IItemSource itemSource)
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
      MediaPathValidator.ValidateMediaPath (path);

      string relativePath = path;
      string result = SessionConfigValidator.AutocompleteInstanceUrl(this.sessionConfig.InstanceUrl);

      string lowerCasePathForComparisonNeeds = path.ToLowerInvariant();

      bool isMediaHookAvailable = lowerCasePathForComparisonNeeds.Contains(MediaItemUrlBuilder.mediaHook);

      int dotPosition = path.LastIndexOf(".");
      bool isExtensionUnavailable = ( -1 == dotPosition );
      bool isExtensionAvailable = !isExtensionUnavailable;

      if (isMediaHookAvailable)
      {
        result = result + this.restGrammar.PathComponentSeparator + Uri.EscapeUriString(relativePath);

        if ( !isExtensionAvailable )
        {
          result = result + MediaItemUrlBuilder.ashxExtension;
        }
      }
      else
      {
        result = result + this.restGrammar.PathComponentSeparator + MediaItemUrlBuilder.mediaHook;

        string mediaLibraryRoot = this.sessionConfig.MediaLybraryRoot.ToLowerInvariant();

        int rootStartIndex = lowerCasePathForComparisonNeeds.IndexOf(mediaLibraryRoot);

        bool isMediaRootAvailable = (rootStartIndex >= 0);

        if ( isMediaRootAvailable )
        {
          relativePath = path.Remove(rootStartIndex, this.sessionConfig.MediaLybraryRoot.Length);
        }


        bool isInvalidRelativePath = string.IsNullOrEmpty(relativePath);
        if (isInvalidRelativePath)
        {
          throw new ArgumentException("ResourceUrlBuilder.BuildUrlStringForPath invalid path");
        }

        relativePath = Uri.EscapeUriString(relativePath);

        result = result + relativePath + MediaItemUrlBuilder.ashxExtension;
      }

      result = this.AppendUrlStringWithDownloadOptions(result, options);

      return result;
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
        paramsString = this.AddOptionValueToPath(paramsString, MediaItemUrlBuilder.databaseKey, this.itemSource.Database);
      }

      if (this.itemSource.Language != null)
      {
        paramsString = this.AddOptionValueToPath(paramsString, MediaItemUrlBuilder.languageKey, this.itemSource.Language);
      }

      if (this.itemSource.Version != null)
      {
        paramsString = this.AddOptionValueToPath(paramsString, MediaItemUrlBuilder.versionKey, this.itemSource.Version);
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

    private const string mediaHook = "~/media";
    private const string ashxExtension  = ".ashx";

    private const string languageKey = "la";
    private const string versionKey = "vs";
    private const string databaseKey = "db";

    private IItemSource itemSource;
    private IRestServiceGrammar restGrammar;
    private ISessionConfig sessionConfig;
  }
}

