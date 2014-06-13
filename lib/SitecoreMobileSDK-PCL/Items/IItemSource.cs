using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sitecore.MobileSDK.Items
{
    public interface IItemSource
    {
        string Database { get; }
        string Language { get; }

        #region Version
        string Version  { get; }
        int VersionNumber { get; }
        #endregion Version
    }
}
