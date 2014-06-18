using System;
using Sitecore.MobileSDK.Utils;
using Sitecore.MobileSDK.SessionSettings;
using System.Collections.Generic;

namespace Sitecore.MobileSDK.MediaItems
{
	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.UrlBuilder.Rest;

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

//		https://test.host/~/media/1.png.ashx?w=640&h=480
		public string BuildUrlStringForPath(string path, DownloadMediaOptions options)
		{
			MediaItemPathValidator.ValidateMediaItemPath (path);
		
			string relativePath = path;
			string result = this.sessionConfig.InstanceUrl;


			bool isMediaHookAvailable = (path.IndexOf (MediaItemUrlBuilder.mediaHook, StringComparison.CurrentCultureIgnoreCase) >= 0);
			bool isExtensionAvailable = (path.IndexOf (MediaItemUrlBuilder.ashxExtension, StringComparison.CurrentCultureIgnoreCase) >= 0);

			if (isMediaHookAvailable)
			{
				result = result + this.restGrammar.PathComponentSeparator + Uri.EscapeUriString (relativePath);

				if ( !isExtensionAvailable )
				{
					result = result + MediaItemUrlBuilder.ashxExtension;
				}
			}
			else
			{
				result = result + this.restGrammar.PathComponentSeparator + MediaItemUrlBuilder.mediaHook;

				int rootStartIndex = path.IndexOf (MediaItemUrlBuilder.mediaRoot, StringComparison.CurrentCultureIgnoreCase);
				bool isMediaRootAvailable = (rootStartIndex >= 0);

				if ( isMediaRootAvailable )
				{
					relativePath = path.Remove(rootStartIndex, MediaItemUrlBuilder.mediaRoot.Length);
				}


				bool isInvalidRelativePath = string.IsNullOrEmpty(relativePath);
				if (isInvalidRelativePath)
				{
					throw new ArgumentException("ResourceUrlBuilder.BuildUrlStringForPath invalid path");
				}

				relativePath = Uri.EscapeUriString (relativePath);

				result = result + relativePath + MediaItemUrlBuilder.ashxExtension;
			}
				
			result = this.AppendUrlStringWithDownloadOptions (result, options);

			return result;
		}

		private string AppendUrlStringWithDownloadOptions(string path, DownloadMediaOptions options)
		{
			if (DownloadMediaOptions.IsEmptyOrEmpty (options))
			{
				return path;
			}

			path += this.restGrammar.HostAndArgsSeparator;

			Dictionary<string, string> optionsDictionary = options.OptionsDictionary;
			foreach (var pair in optionsDictionary)
			{
				path = this.AddOptionValueToPath (path, pair.Key, pair.Value);
			}

			if (this.itemSource.Database != null)
			{
				path = this.AddOptionValueToPath (path, MediaItemUrlBuilder.databaseKey, this.itemSource.Database);
			}

			if (this.itemSource.Language != null)
			{
				path = this.AddOptionValueToPath (path, MediaItemUrlBuilder.languageKey, this.itemSource.Language);
			}

			if (this.itemSource.Version != null)
			{
				path = this.AddOptionValueToPath (path, MediaItemUrlBuilder.versionKey, this.itemSource.Version);
			}

			return path = path.Remove(path.Length - this.restGrammar.FieldSeparator.Length);

		}

		private string AddOptionValueToPath(string path, string key, string value)
		{
			return path += key + this.restGrammar.KeyValuePairSeparator + value + this.restGrammar.FieldSeparator;
		}
			
		private const string mediaHook 		= "~/media";
		private const string ashxExtension 	= ".ashx";

		private const string languageKey 		= "la";
		private const string versionKey 		= "vs";
		private const string databaseKey 		= "db";

		private IItemSource 		itemSource;
		private IRestServiceGrammar restGrammar;
		private ISessionConfig 		sessionConfig;
	}
}

