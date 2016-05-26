namespace Sitecore.MobileSDK.Validators
{
  using System;

  public static class ItemIdValidator
  {
    private const string INVALID_ID_MESSAGE = "Item id lenght must be 36";
    private const string EMPTY_ERROR_MESSAGE = "Item can not be empty";


    public static void ValidateItemId(string itemId, string source)
    {
      //TODO: @igk investigate and test in unit tests base id criterias, no info in documentation is available
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(itemId, source);
    
      if (itemId.Length != 36)
      {
        throw new ArgumentException(source + " : " + INVALID_ID_MESSAGE);
      }
    }

    public static void ValidateSearchRequest(string term, string source)
    {
      //TODO: @igk investigate and test in unit tests base id criterias, no info in documentation is available
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(term, source);
    }
  }
}

