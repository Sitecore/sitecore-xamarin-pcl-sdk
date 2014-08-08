namespace Sitecore.MobileSDK.Validators
{
  using System;
  using Sitecore.MobileSDK.API;


  public class MediaPathValidator
  {
    private IMediaLibrarySettings mediaSettings;

    public MediaPathValidator(IMediaLibrarySettings mediaSettings)
    {
      this.mediaSettings = mediaSettings;
    }

    public static void ValidateMediaRoot(string mediaLibraryRootItemPath, string source)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(mediaLibraryRootItemPath, source);

      string lowerCasePath = mediaLibraryRootItemPath.ToLowerInvariant();
      bool isValidPathPrefix = lowerCasePath.StartsWith("/");
      if (!isValidPathPrefix)
      {
        throw new ArgumentException(source + " : Path must begin with '/'");
      }
    }

    public void ValidateMediaPath(string itemPath, string source)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemPath, source);

      bool isAbsoluteItemPath = IsItemPathStartsWithSlash(itemPath);
      bool isMediaHookPresent = this.IsItemPathHasMediaHook(itemPath);
      bool isValidPrefix = isAbsoluteItemPath || isMediaHookPresent;
      if (!isValidPrefix)
      {
        throw new ArgumentException(source + " : Should begin with '/' or '~'");
      }

      if (isAbsoluteItemPath)
      {
        bool hasExtension = IsItemPathHasExtension(itemPath);
        if (hasExtension)
        {
          throw new ArgumentException(source + " : Starting with '/' cannot have an extension (.ashx and others)");
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
      string lowerCaseHook = this.mediaSettings.MediaPrefix.ToLowerInvariant();
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
      bool isDotNotFound = (-1 == dotIndex);

      bool isExtensionAvailable = !isDotNotFound;
      return isExtensionAvailable;
    }
  }
}

