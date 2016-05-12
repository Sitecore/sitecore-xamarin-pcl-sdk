namespace Sitecore.MobileSDK.API.Items
{
  using System;
  using System.Collections.Generic;
  using Sitecore.MobileSDK.API.Fields;

  /// <summary>
  /// This interface represents a Sitecore item. An item is an element of the content tree stored in a particular database. Depending on its localization language and version its fields may contain different content.
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
    /// Returns item's GUID.
    /// For example: "110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9"
    /// 
    /// The value is case insensitive.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Returns item's path in the content tree. 
    /// For example: "/sitecore/content/Home"
    /// 
    /// The value is case insensitive.
    /// </summary>
    string Path { get; }

    /// <summary>
    /// Returns template's GUID.
    /// For example: "76036f5e-cbce-46d1-af0a-4143f9b557aa"
    /// 
    /// The value is case insensitive.
    /// </summary>
    string TemplateId { get; }

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

