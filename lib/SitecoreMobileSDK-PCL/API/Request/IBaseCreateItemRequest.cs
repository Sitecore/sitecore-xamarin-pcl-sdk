namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents basic create item request parameters.
  /// <seealso cref="ICreateItemByIdRequest"/>
  /// <seealso cref="ICreateItemByPathRequest"/>
  /// </summary>
  public interface IBaseCreateItemRequest : IBaseChangeItemRequest
  {
    /// <summary>
    /// Gets the name of the item. Represents name of the item in the content tree.
    /// 
    /// The value is case sensitive.
    /// </summary>
    /// <returns>
    /// The name of the item.
    /// </returns>
    string ItemName { get; }

    //TODO: fix template description, id instead of path
    /// <summary>
    /// A relative path to the item's template. 
    /// The path is relative to the "/sitecore/templates" item.
    /// For example: "Common/Folder".
    /// 
    /// The value is case insensitive.
    /// </summary>
    /// <returns>
    /// The item template.
    /// </returns>
    string ItemTemplateId { get; }
  }
}

