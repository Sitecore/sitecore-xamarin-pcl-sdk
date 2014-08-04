namespace Sitecore.MobileSDK.Validators
{
  using System;

  public static class ItemPathValidator
  {
    public static void ValidateItemPath(string itemPath, string source)
    {
      ItemPathValidator.CommonValidatePath (itemPath, source, "Item name");
    }

    public static void ValidateItemTemplate(string itemPath, string source)
    {
      ItemPathValidator.CommonValidatePath (itemPath, source, "Template");
    }

    private static void CommonValidatePath(string itemPath, string source, string parameterName)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemPath, source);

      if (!itemPath.StartsWith("/"))
      {
        throw new ArgumentException(source + " : " + parameterName + " should begin with '/'");
      }

      char[] unexpectedSymbols = { '.' };
      int unexpectedSymbolIndex = itemPath.IndexOfAny(unexpectedSymbols);
      bool hasUnexpectedSymbols = (-1 != unexpectedSymbolIndex);
      if (hasUnexpectedSymbols)
      {
        var unexpectedSymbol = itemPath[unexpectedSymbolIndex];
        string message = string.Format("{0} : " + parameterName + " cannot contain '{1}'", source, unexpectedSymbol);

        throw new ArgumentException(message);
      }
    }
  }
}

