namespace Sitecore.MobileSDK.API.Request
{
  using System.Collections.Generic;

  /// <summary>
  /// Interface that represents necessary data for requests that change item.
  /// <para><see cref="IUpdateItemByIdRequest"/></para>
  /// <para><see cref="IUpdateItemByPathRequest"/></para>
  /// <para><see cref="ICreateItemByIdRequest"/></para>
  /// <para><see cref="ICreateItemByPathRequest"/></para>
  /// </summary>
  public interface IBaseChangeItemRequest : IBaseItemRequest
  {
    /// <summary>
    /// Gets the fields that will be updated or created.
    /// </summary>
    /// <returns>
    /// Field name , field raw value pairs.
    /// </returns>
    IDictionary<string, string> FieldsRawValuesByName { get; }
  }
}

