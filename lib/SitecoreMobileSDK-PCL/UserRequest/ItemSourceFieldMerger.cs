
namespace Sitecore.MobileSDK.UserRequest
{
  using System;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.Items;

  public class ItemSourceFieldMerger
  {
    public ItemSourceFieldMerger (IItemSource defaultSource)
    {
      if (null == defaultSource)
      {
        return;
      }

      this.defaultSource = defaultSource.ShallowCopy();
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
      int? version  = (null != userSource.VersionNumber ) ? userSource.VersionNumber  : this.defaultSource.VersionNumber;


      return new ItemSource(database, language, version);
    }

    public IItemSource DefaultSource
    { 
      get
      {
        return this.defaultSource;
      }
    }

    private readonly IItemSource defaultSource;
  }
}

