

namespace Sitecore.MobileSDK.API.Items
{
    public interface IItemSource
  {
    IItemSource ShallowCopy();

    string Database { get; }
    string Language { get; }

    #region Version
    int? VersionNumber { get; }
    #endregion Version
  }
}
