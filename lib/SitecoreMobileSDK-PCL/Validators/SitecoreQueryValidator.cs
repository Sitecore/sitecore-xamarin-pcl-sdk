namespace Sitecore.MobileSDK.Validators
{
  public static class SitecoreQueryValidator
  {
    public static void ValidateSitecoreQuery(string sitecoreQuery, string source)
    {
      BaseValidator.CheckForNullEmptyAndWhiteSpaceOrThrow(sitecoreQuery, source);
    }
  }
}

