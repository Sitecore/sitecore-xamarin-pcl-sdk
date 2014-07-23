
namespace Sitecore.MobileSDK.Validators
{
  using System;

  public static class ItemPathValidator
  {
    public static void ValidateItemPath(string itemPath)
    {
      if ( string.IsNullOrWhiteSpace(itemPath) )
      {
        throw new ArgumentException("Item path cannot be null or empty");
      }
      else if (!itemPath.StartsWith("/"))
      {
        throw new ArgumentException("Item path should begin with '/'");
      }


      char[] unexpectedSymbols = { '.' };
      int unexpectedSymbolIndex = itemPath.IndexOfAny(unexpectedSymbols);
      bool hasUnexpectedSymbols = (-1 != unexpectedSymbolIndex);
      if (hasUnexpectedSymbols)
      {
        var unexpectedSymbol = itemPath[unexpectedSymbolIndex];
        string message = string.Format("Item path cannot contain '{0}'", unexpectedSymbol);

        throw new ArgumentException(message);
      }

    }
  }
}

