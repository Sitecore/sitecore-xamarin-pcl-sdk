using System;

namespace Sitecore.MobileSDK.Items
{
    using Sitecore.MobileSDK.API.Items;

    public class ItemSource : ItemSourcePOD
  {
    public const string DefaultDatabase = "web";
    public const string DefaultLanguage = "en";

    public static ItemSource DefaultSource()
    {
      return new ItemSource (ItemSource.DefaultDatabase, ItemSource.DefaultLanguage, null);
    }

    public ItemSource(string database, string language, string version = null) 
      : base(database, language, version)
    {
    }

    public override IItemSource ShallowCopy()
    {
      return new ItemSource(this.Database, this.Language, this.Version);
    }
  } 
}

