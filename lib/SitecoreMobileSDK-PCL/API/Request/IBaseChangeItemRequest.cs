namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;

  /// <summary>
  /// Interface that represents necessary data for requests that change item.
  /// <param><see cref="IUpdateItemByIdRequest"/></param>
  /// <param><see cref="IUpdateItemByPathRequest"/></param>
  /// <param><see cref="ICreateItemByIdRequest"/></param>
  /// <param><see cref="ICreateItemByPathRequest"/></param>
  /// </summary>
  public interface IBaseChangeItemRequest : IBaseItemRequest
  {
    /// <summary>
    /// Gets the fields that will be updated or created.
    /// key   - must contain field name.
    /// value - must contain new field raw value.
    /// </summary>
    /// <returns>
    /// Field name, field raw value pairs.
    /// </returns>
    IDictionary<string, string> FieldsRawValuesByName { get; }
  }
}

