namespace Sitecore.MobileSDK.API.Items
{
  /// <summary>
  /// This class represents source config of <see cref="ISitecoreItem"/>.
  /// 
  /// It contains information about a database that contains the item. 
  /// For example,
  /// * master
  /// * web
  /// * core
  /// 
  /// Since the CMS serves localized content, this class also provides the language of the given item. An abbreviation of two letters is used. For example,
  /// * en
  /// * da
  /// * fr
  /// * ja
  /// * cn
  /// 
  /// 
  /// As content editing is usually performed in a particular workflow, the IItemSource class stores the item's version. It is either a positive integer number or a latest version (null).
  /// 
  /// 
  /// Item source indicates the origin of a given item. It is also used in requests to define the place of a given CRUD operation.
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

    /// <summary>
    /// Returns item version. It is a positive integer number.
    /// A null value stands for the "latest" version.
    /// </summary>
    int? VersionNumber { get; }
  }
}
