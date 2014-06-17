using System;
using Sitecore.MobileSDK.Utils;
using Sitecore.MobileSDK.SessionSettings;

namespace Sitecore.MobileSDK.MediaItems
{
	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.UrlBuilder.Rest;

	public class ResourceUrlBuilder
	{
		public ResourceUrlBuilder ()
		{
		}

		public ResourceUrlBuilder(IRestServiceGrammar restGrammar, ISessionConfig sessionConfig, IItemSource itemSource)
		{
			this.itemSource = itemSource;
			this.restGrammar = restGrammar;
			this.sessionConfig = sessionConfig;

			//this.Validate();
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
			if (null == path)
			{
				throw new ArgumentNullException("ResourceUrlBuilder.BuildUrlStringForPath path cannot be null");
			}

			string relativePath = path;
			string result = this.sessionConfig.InstanceUrl;


			bool isMediaHookAvailable = (path.IndexOf (ResourceUrlBuilder.mediaHook, StringComparison.CurrentCultureIgnoreCase) >= 0);
			bool isExtensionAvailable = (path.IndexOf (ResourceUrlBuilder.ashxExtension, StringComparison.CurrentCultureIgnoreCase) >= 0);

			if (isMediaHookAvailable)
			{
				result = result + this.restGrammar.PathComponentSeparator + Uri.EscapeUriString (relativePath);

				if ( !isExtensionAvailable )
				{
					result = result + ResourceUrlBuilder.ashxExtension;
				}
			}
			else
			{
				result = result + this.restGrammar.PathComponentSeparator + ResourceUrlBuilder.mediaHook;

				int rootStartIndex = path.IndexOf (ResourceUrlBuilder.mediaRoot, StringComparison.CurrentCultureIgnoreCase);
				bool isMediaRootAvailable = (rootStartIndex >= 0);

				if ( isMediaRootAvailable )
				{
					relativePath = path.Remove(rootStartIndex, ResourceUrlBuilder.mediaRoot.Length);
				}


				bool isInvalidRelativePath = string.IsNullOrEmpty(relativePath);
				if (isInvalidRelativePath)
				{
					throw new ArgumentException("ResourceUrlBuilder.BuildUrlStringForPath invalid path");
				}

				relativePath = Uri.EscapeUriString (relativePath);

				result = result + relativePath + ResourceUrlBuilder.ashxExtension;
			}

//			string result =
//				this.webApiGrammar.DatabaseParameterName + this.restGrammar.KeyValuePairSeparator + escapedDatabase +
//
//				this.restGrammar.FieldSeparator +
//				this.webApiGrammar.LanguageParameterName + this.restGrammar.KeyValuePairSeparator + escapedLanguage;
//
//			if (null != this.itemSource.Version)
//			{
//				string escapedVersion = UrlBuilderUtils.EscapeDataString(this.itemSource.Version);
//
//				result +=
//					this.restGrammar.FieldSeparator +
//					this.webApiGrammar.VersionParameterName + this.restGrammar.KeyValuePairSeparator + escapedVersion;
//			}
//
			return result;
		}

		//TODO: @igk move to Session!!!
		private const string mediaRoot = "/sitecore/media library";

		private const string mediaHook = "~/media";
		private const string ashxExtension = ".ashx";
		private IItemSource itemSource;
		private IRestServiceGrammar restGrammar;
		private ISessionConfig sessionConfig;
	}
}

