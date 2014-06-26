

namespace Sitecore.MobileSDK.Items
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;

  public interface IItemSource
  {
    IItemSource ShallowCopy();

    string Database { get; }
    string Language { get; }

    #region Version
    string Version  { get; }
    int VersionNumber { get; }
    #endregion Version
  }
}
