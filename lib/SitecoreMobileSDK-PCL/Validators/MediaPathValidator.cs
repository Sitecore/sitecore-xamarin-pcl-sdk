namespace Sitecore.MobileSDK.Validators
{
  using System;
  using Sitecore.MobileSDK.API;


	public class MediaPathValidator
	{
    private ISessionConfig sessionConfig;

    public MediaPathValidator(ISessionConfig sessionConfig)
    {
      this.sessionConfig = sessionConfig;
    }

    public static void ValidateMediaRoot(string mediaLibraryRootItemPath)
    {
      if ( string.IsNullOrWhiteSpace(mediaLibraryRootItemPath) )
      {
        throw new ArgumentException("Media path cannot be null or empty");
      }

      string lowerCasePath = mediaLibraryRootItemPath.ToLowerInvariant();
      bool isValidPathPrefix = lowerCasePath.StartsWith("/");
      if (!isValidPathPrefix)
      {
        throw new ArgumentException("Media Library Root path must begin with '/'");
      }
    }

		public void ValidateMediaPath(string itemPath)
		{
			if ( string.IsNullOrWhiteSpace(itemPath) )
			{
        throw new ArgumentException("Media path cannot be null or empty");
			}

      bool isAbsoluteItemPath = MediaPathValidator.IsItemPathStartsWithSlash(itemPath);
      bool isMediaHookPresent = this.IsItemPathHasMediaHook(itemPath);
      bool isValidPrefix = isAbsoluteItemPath || isMediaHookPresent;
      if (!isValidPrefix)
			{
				throw new ArgumentException("Media path should begin with '/' or '~'");
			}

      if (isAbsoluteItemPath)
      {
        bool hasExtension = MediaPathValidator.IsItemPathHasExtension(itemPath);
        if (hasExtension)
        {
          throw new ArgumentException("Item path starting with '/' cannot have an extension (.ashx and others)");
        }
      }
		}

    private static bool IsItemPathStartsWithSlash(string itemPath)
    {
      bool isAbsoluteItemPath = itemPath.StartsWith("/");
      return isAbsoluteItemPath;
    }

    private bool IsItemPathHasMediaHook(string itemPath)
    {
      string lowerCasePath = itemPath.ToLowerInvariant();
      string lowerCaseHook = this.sessionConfig.MediaPrefix.ToLowerInvariant();
      if (!lowerCaseHook.EndsWith("/"))
      {
        lowerCaseHook += "/";
      }

      bool isMediaHookPresent = lowerCasePath.StartsWith(lowerCaseHook);
      return isMediaHookPresent;
    }

    public static bool IsItemPathHasExtension(string itemPath)
    {
      int dotIndex = itemPath.LastIndexOf(".");
      bool isDotNotFound = ( -1 == dotIndex );

      bool isExtensionAvailable = !isDotNotFound;
      return isExtensionAvailable;
    }
	}
}

