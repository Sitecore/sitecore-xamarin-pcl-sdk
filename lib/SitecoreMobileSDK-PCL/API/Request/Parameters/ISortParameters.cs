namespace Sitecore.MobileSDK.API.Request.Parameters
{
  using System.Collections.Generic;

  public interface ISortParameters
  {
    /// <summary>
    /// Performs deep the copy of <see cref="IQueryParameters"/>.
    /// </summary>
    /// <returns><seealso cref="IQueryParameters"/></returns>
    ISortParameters DeepCopy();

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
