
namespace Sitecore.MobileSDK
{
  using System;
  using Sitecore.MobileSDK.Items;

  public class ItemSourceFieldMerger
  {
    public ItemSourceFieldMerger (IItemSource defaultSource)
    {
      this.defaultSource = defaultSource;
    }

    public IItemSource FillItemSourceGaps(IItemSource userSource)
    {
      bool isNullSource = (null == this.defaultSource);
      bool isNullInput = (null == userSource);

      if (isNullSource && isNullInput)
      {
        return null;
      }
      else if (isNullInput)
      {
        return this.defaultSource.ShallowCopy();
      }
      else if (isNullSource)
      {
        return userSource.ShallowCopy();
      }


      string database = (null != userSource.Database) ? userSource.Database : this.defaultSource.Database;
      string language = (null != userSource.Language) ? userSource.Language : this.defaultSource.Language;
      string version  = (null != userSource.Version ) ? userSource.Version  : this.defaultSource.Version ;


      return new ItemSource (database, language, version);
    }

    private readonly IItemSource defaultSource;
  }
}

