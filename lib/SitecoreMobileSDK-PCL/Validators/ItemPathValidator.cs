namespace Sitecore.MobileSDK.Validators
{
  using System;

  public static class ItemPathValidator
  {
    public static void ValidateItemPath(string itemPath, string source)
    {
      if (string.IsNullOrWhiteSpace(itemPath))
      {
        BaseValidator.ThrowNullOrEmptyParameterException(source);
      }
      else if (!itemPath.StartsWith("/"))
      {
        throw new ArgumentException(source + " : Item path should begin with '/'");
      }


      char[] unexpectedSymbols = { '.' };
      int unexpectedSymbolIndex = itemPath.IndexOfAny(unexpectedSymbols);
      bool hasUnexpectedSymbols = (-1 != unexpectedSymbolIndex);
      if (hasUnexpectedSymbols)
      {
        var unexpectedSymbol = itemPath[unexpectedSymbolIndex];
        string message = string.Format("{0} : Item path cannot contain '{1}'", source, unexpectedSymbol);

        throw new ArgumentException(message);
      }

    }
  }
}

