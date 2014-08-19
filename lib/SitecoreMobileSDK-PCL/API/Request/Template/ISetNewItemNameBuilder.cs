namespace Sitecore.MobileSDK.API.Request.Template
{
  /// <summary>
  /// Interface that specifies flow for creation of request to create item.
  /// </summary>
  /// <typeparam name="T">Type of request</typeparam>
  public interface ISetNewItemNameBuilder<T> where T : class
  {
    /// <summary>
    /// Specifies item's name.
    /// </summary>
    /// <param name="itemName">Item's name.
    /// The value is case sensitive.
    /// </param>
    /// <returns><see cref="ICreateItemRequestParametersBuilder{T}"/></returns>
    ICreateItemRequestParametersBuilder<T> ItemName(string itemName);
  }
}

