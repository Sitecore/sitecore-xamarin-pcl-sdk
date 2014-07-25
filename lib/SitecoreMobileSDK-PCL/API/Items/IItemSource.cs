

namespace Sitecore.MobileSDK.API.Items
{
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
