namespace Sitecore.MobileSDK.Validators
{
  using System;

  public static class ItemPathValidator
  {
    public static void ValidateItemPath(string itemPath, string source)
    {
      ItemPathValidator.CommonValidatePath(itemPath, source);

      if (!itemPath.StartsWith("/"))
      {
        throw new ArgumentException(source + " : should begin with '/'");
      }
    }

    public static void ValidateItemTemplate(string itemPath, string source)
    {
      ItemPathValidator.CommonValidatePath(itemPath, source);
    }

    private static void CommonValidatePath(string itemPath, string source)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemPath, source);

      char[] unexpectedSymbols = { '.' };
      int unexpectedSymbolIndex = itemPath.IndexOfAny(unexpectedSymbols);
      bool hasUnexpectedSymbols = (-1 != unexpectedSymbolIndex);
      if (hasUnexpectedSymbols)
      {
        var unexpectedSymbol = itemPath[unexpectedSymbolIndex];
        string message = string.Format("{0} : cannot contain '{1}'", source, unexpectedSymbol);

        throw new ArgumentException(message);
      }
    }

    public static void ValidateMediaItemPath(string mediaPath, string source)
    {
      if (!string.IsNullOrEmpty(mediaPath))
      {
        CommonValidatePath(mediaPath, source);
      }
    }
  }
}

