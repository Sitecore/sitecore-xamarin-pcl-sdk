
namespace Sitecore.MobileSDK.Items
{
  using System;
  using Sitecore.MobileSDK.API.Items;

  public class ItemSource : ItemSourcePOD
  {
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

