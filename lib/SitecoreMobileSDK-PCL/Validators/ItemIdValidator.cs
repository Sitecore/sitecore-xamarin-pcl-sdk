namespace Sitecore.MobileSDK.Validators
{
  using System;

  public static class ItemIdValidator
  {
    private const string INVALID_ID_MESSAGE = "Item id must have curly braces '{}'";
    public static void ValidateItemId(string itemId, string source)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemId, source);

      bool hasOpeningBrace = itemId.StartsWith("{");
      bool hasClosingBrace = itemId.EndsWith("}");
      bool hasNonBraceSymbols = (itemId.Length > 2);

      bool isValidId = hasOpeningBrace && hasClosingBrace && hasNonBraceSymbols;
      if (!isValidId)
      {
        throw new ArgumentException(source + " : " + INVALID_ID_MESSAGE);
      }
    }
  }
}

