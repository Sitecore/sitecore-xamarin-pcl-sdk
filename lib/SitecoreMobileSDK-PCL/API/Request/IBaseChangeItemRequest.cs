namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;

  /// <summary>
  /// Interface that represents necessary data for requests that change item.
  /// <param><seealso cref="IUpdateItemByIdRequest"/></param>
  /// <param><seealso cref="IUpdateItemByPathRequest"/></param>
  /// <param><seealso cref="ICreateItemByIdRequest"/></param>
  /// <param><seealso cref="ICreateItemByPathRequest"/></param>
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

