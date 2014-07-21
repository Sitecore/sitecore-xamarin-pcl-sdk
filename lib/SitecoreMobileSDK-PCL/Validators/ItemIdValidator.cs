namespace Sitecore.MobileSDK.Validators
{
  using System;

  public class ItemIdValidator
  {
    private ItemIdValidator ()
    {
    }

    public static void ValidateItemId(string itemId)
    {
      if (string.IsNullOrWhiteSpace(itemId))
      {
        throw new ArgumentException("Item id cannot be null");
      }

      bool hasOpeningBrace = itemId.StartsWith("{");
      bool hasClosingBrace = itemId.EndsWith("}");
      bool hasNonBraceSymbols = (itemId.Length > 2);

      bool isValidId = hasOpeningBrace && hasClosingBrace && hasNonBraceSymbols;
      if (!isValidId)
      {
        throw new ArgumentException("Item id must have curly braces '{}'");
      }
    }
  }
}

