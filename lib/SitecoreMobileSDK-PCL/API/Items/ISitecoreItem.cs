namespace Sitecore.MobileSDK.API.Items
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Fields;

  /// <summary>
  /// This interace represents a Sitecore item. An item is an element of the content tree stored in a particular database. Depending on its localization language and version its fields may contain different content.
  /// 
  /// Items are readonly and immutable. If you need to modify the content you should send a new request using the session.
  /// </summary>
  public interface ISitecoreItem
  {
    /// <summary>
    /// Returns item's <see cref="IItemSource"/>
    /// </summary>
    IItemSource Source { get; }

    /// <summary>
    /// Returns item's display name. It is a value of the "__Display name" property if it exists. Otherwise it is a name of the item in the content tree.
    /// </summary>
    string DisplayName { get; }

    /// <summary>
    /// Indicates whether item has children. The flag is supplied by the Item Web API service.
    /// </summary>
    bool HasChildren { get; }

    /// <summary>
    /// Returns item's GUID. Item Web API service returns GUID values enclosed in curly braces.
    /// For example : "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"
    /// 
    /// The value is case insensitive.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Returns item's long id. LongId is similar to the Path property. However, it contains of GUIDs separated by the slash symbol.
    /// 
    /// For example, "/{11111111-1111-1111-1111-111111111111}/{0DE95AE4-41AB-4D01-9EB0-67441B7C2450}/{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"
    /// 
    /// The value is case insensitive.
    /// </summary>
    string LongId { get; }

    /// <summary>
    /// Returns item's path in the content tree. For example, "/sitecore/content/Home"
    /// The value is case insensitive.
    /// </summary>
    string Path { get; }

    /// <summary>
    /// Returns a relative path to the item's template. For example, "Common/Folder".
    /// The path is relative to the "/sitecore/templates" item.
    /// The value is case insensitive.
    /// </summary>
    string Template { get; }

    /// <summary>
    /// Returns number of downloaded fields for the given item.
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
    /// Returns item's fields for enumeration. This property should be used both with the foreach loop and linq extensions.
    /// </summary>
    IEnumerable<IField> Fields{ get; }
  }
}

