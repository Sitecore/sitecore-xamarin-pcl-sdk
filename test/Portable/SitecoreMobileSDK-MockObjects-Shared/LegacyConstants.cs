namespace Sitecore.MobileSDK.MockObjects
{
  using Sitecore.MobileSDK.Items;

  public static class LegacyConstants
  {
    public const string DefaultDatabase = "web";
    public const string DefaultLanguage = "en";


    public static ItemSource DefaultSource()
    {
      return new ItemSource(DefaultDatabase, DefaultLanguage, null);
    }
  }
}

