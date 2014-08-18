namespace Sitecore.MobileSDK.API.Items
{
  /// <summary>
  /// Class represents source config of <see cref="ISitecoreItem"/>.  
  /// </summary>
  public interface IItemSource
  {
    /// <summary>
    /// Returns copy of <see cref="IItemSource"/>.
    /// </summary>
    IItemSource ShallowCopy();

    /// <summary>
    /// Returns item database.
    /// </summary>
    string Database { get; }

    /// <summary>
    /// Returns item language.
    /// </summary>
    string Language { get; }

    #region Version
    /// <summary>
    /// Returns item version.
    /// </summary>
    int? VersionNumber { get; }
    #endregion Version
  }
}
