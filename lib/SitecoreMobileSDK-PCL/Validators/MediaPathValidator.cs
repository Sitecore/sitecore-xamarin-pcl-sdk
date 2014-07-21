namespace Sitecore.MobileSDK.Validators
{
  using System;

	public class MediaPathValidator
	{
		public MediaPathValidator ()
		{
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

		public static void ValidateMediaPath(string itemPath)
		{
			if ( string.IsNullOrWhiteSpace(itemPath) )
			{
        throw new ArgumentException("Media path cannot be null or empty");
			}

      bool isAbsoluteItemPath = itemPath.StartsWith("/");
      bool isMediaHookPresent = itemPath.StartsWith("~");
      bool isValidPrefix = isAbsoluteItemPath || isMediaHookPresent;
      if (!isValidPrefix)
			{
				throw new ArgumentException("Media path should begin with '/' or '~'");
			}
		}
	}
}

