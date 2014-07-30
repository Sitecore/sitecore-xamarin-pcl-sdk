namespace Sitecore.MobileSDK.Validators
{
  public static class SitecoreQueryValidator
  {
    public static void ValidateSitecoreQuery(string sitecoreQuery, string source)
    {
      if ( string.IsNullOrWhiteSpace(sitecoreQuery) )
      {
        BaseValidator.ThrowNullOrEmptyParameterException(source); 
      }
    }
  }
}

