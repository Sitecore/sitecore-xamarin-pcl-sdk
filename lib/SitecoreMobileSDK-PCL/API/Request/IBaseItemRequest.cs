namespace Sitecore.MobileSDK.API.Request
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;

  /// <summary>
  /// Inteface represents basic parameters neccesessary for items requests.
  /// </summary>
  public interface IBaseItemRequest
  {
    /// <summary>
    /// Performs deep copy of <see cref="IBaseItemRequest"/>.
    /// </summary>
    /// <returns><seealso cref="IBaseItemRequest"/></returns>
    IBaseItemRequest DeepCopyBaseGetItemRequest();

    /// <summary>
    /// Gets the item source.
    /// </summary>
    /// <value>
    /// The item source.
    /// </value>
    /// <seealso cref="IItemSource" />
    IItemSource ItemSource { get; }

    /// <summary>
    /// Gets the session settings.
    /// </summary>
    /// <value>
    /// The session settings.
    /// </value>
    /// <seealso cref="ISessionConfig" />
    ISessionConfig SessionSettings { get; }

    /// <summary>
    /// Gets the query parameters.
    /// </summary>
    /// <value>
    /// The query parameters.
    /// </value>
    /// <seealso cref="IQueryParameters" />
    IQueryParameters QueryParameters { get; }
  }
}
