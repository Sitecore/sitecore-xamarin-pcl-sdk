
namespace SitecoreMobileSDKMockObjects
{
  using System;
  using Sitecore.MobileSDK.Items;

  public class MutableItemSource : ItemSource
  {
    public MutableItemSource(string database, string language, string version = null) 
      : base(database, language, version)
    {
    }

    public void SetDatabase(string value)
    {
      this.Database = value;
    }

    public void SetLanguage(string value)
    {
      this.Language = value;
    }

    public void SetVersion(string value)
    {
      this.Version = value;
    }
  }
}

