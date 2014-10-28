namespace Sitecore.MobileSDK.Items
{
  using Sitecore.MobileSDK.API.Items;

  public class ItemSource : ItemSourcePOD
  {
    public ItemSource(string database, string language, int? version = null)
      : base(database, language, version)
    {
    }

    public override IItemSource ShallowCopy()
    {
      return new ItemSource(this.Database, this.Language, this.VersionNumber);
    }
  }
}

