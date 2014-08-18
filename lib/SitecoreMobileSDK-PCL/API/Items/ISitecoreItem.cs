namespace Sitecore.MobileSDK.API.Items
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Fields;

  /// <summary>
  /// This interace represents a Sitecore item.
  /// </summary>
  public interface ISitecoreItem
  {
    /// <summary>
    /// Returns item's <see cref="IItemSource"/>
    /// </summary>
    IItemSource Source { get; }

    /// <summary>
    /// Returns item's display name.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Indicates whether item has children.
    /// </summary>
    bool HasChildren { get; }

    /// <summary>
    /// Returns item's GUID.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Returns item's long id.
    /// </summary>
    string LongId { get; }

    /// <summary>
    /// Returns item's path.
    /// </summary>
    string Path { get; }

    /// <summary>
    /// Returns item's display name.
    /// </summary>
    string Template { get; }

    /// <summary>
    /// Returns number of item's fields.
    /// </summary>
    int FieldsCount { get; }

    /// <summary>
    ///     Gets the field with the specified name.
    /// </summary>
    /// <param name="caseInsensitiveFieldName"> The name of the field to get.</param>
    ///
    /// <returns>
    ///     The  <see cref="IField"/> with specified name.
    /// </returns>
    /// 
    /// <exception cref="ArgumentNullException">Name is null</exception>
    /// 
    /// <exception cref="KeyNotFoundException">Name is not found</exception>
    IField this[string caseInsensitiveFieldName] { get; }

    /// <summary>
    /// Returns item's fields.
    /// </summary>
    IEnumerable<IField> Fields{ get; }
  }
}

