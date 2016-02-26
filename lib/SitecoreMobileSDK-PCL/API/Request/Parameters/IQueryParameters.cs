namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System.Collections.Generic;

  /// <summary>
  /// Interface represents 3 parameters needed for read item requests:
  /// <para><c>Scope</c></para><para><c>Payload</c></para><para><c>Fields</c></para>
  /// </summary>
  public interface IQueryParameters
  {
    /// <summary>
    /// Performs deep the copy of <see cref="IQueryParameters"/>.
    /// </summary>
    /// <returns><seealso cref="IQueryParameters"/></returns>
    IQueryParameters DeepCopy();

    /// <summary>
    /// Gets the fields. Determines custom fields scope to retrieve from server for every item.
    /// </summary>
    /// <value>
    /// The fields.
    /// </value>
    /// <returns><seealso cref="IEnumerable{T}"/></returns>
    IEnumerable<string> Fields { get; }
  }
}
