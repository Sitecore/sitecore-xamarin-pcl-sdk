namespace Sitecore.MobileSDK.API.Items
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Class represents server response for delete items request.
  /// It contains a list of ids for the successfully removed items.
  /// 
  /// Note: the mentioned ids can be used to update the persistent cache data.
  /// The cache implementation is not provided by the Sitecore Mobile SDK yet.
  /// </summary>
  public class ScDeleteItemsResponse : IEnumerable<string>
  {
    private List<string> ItemsIds { get; set; }

    public ScDeleteItemsResponse()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScDeleteItemsResponse"/> class.
    /// 
    /// Note: All requests should be returned by the session object. The user is not supposed to construct response classes directly.
    /// </summary>
    /// <param name="itemsIds">List of item ids that were deleted.</param>
    public ScDeleteItemsResponse(List<string> itemsIds)
    {
      this.ItemsIds = itemsIds;
    }

    /// <summary>
    /// Returns amount of ids for items that have been deleted.
    /// </summary>
    public int Count
    {
      get
      {
        return this.ItemsIds.Count;
      }
    }

    #region IEnumerable
    /// <summary>
    ///     Gets the item GUID that was deleted. 
    /// </summary>
    /// <param name="index">The index of item id. The indexation starts with zero.</param>
    ///
    /// <returns>
    ///     The  item GUID. Item Web API service returns GUID values enclosed in curly braces.
    /// For example : "{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}"
    /// 
    /// Field's id is case insensitive.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"> index is less than 0 or index is equal to or greater than <see cref="List{T}.Count"/>.</exception>
    public string this[int index]
    {
      get
      {
        return this.ItemsIds[index];
      }
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the item ids list.
    /// </summary> 
    /// <returns>
    ///     <see cref="IEnumerator{T}"/> that can be used to iterate through the item ids.
    /// </returns>
    public IEnumerator<string> GetEnumerator()
    {
      return this.ItemsIds.GetEnumerator();
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the item ids list. Not generic version.
    /// </summary>
    /// <returns>
    ///      <see cref="IEnumerator{T}"/> that can be used to iterate through the item ids.
    /// </returns>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.ItemsIds.GetEnumerator();
    }
    #endregion IEnumerable
  }
}
