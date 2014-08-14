namespace Sitecore.MobileSDK.API.Request
{
  /// <summary>
  /// Interface represents basic create item request parameters.
  /// <see cref="ICreateItemByIdRequest"/>
  /// <see cref="ICreateItemByPathRequest"/>
  /// </summary>
  public interface IBaseCreateItemRequest : IBaseChangeItemRequest
  {
    /// <summary>
    /// Gets the name of the item.
    /// </summary>
    /// <returns>
    /// The name of the item.
    /// </returns>>
    string ItemName { get; }

    /// <summary>
    /// Gets the item template.
    /// </summary>
    /// <returns>>
    /// The item template.
    /// </returns>>
    string ItemTemplate { get; }
  }
}

