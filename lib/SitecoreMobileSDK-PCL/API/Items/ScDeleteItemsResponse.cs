namespace Sitecore.MobileSDK.API.Items
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Class represents server response for delete items request.
  /// </summary>
  public class ScDeleteItemsResponse : IEnumerable<string>
  {
    private List<string> ItemsIds { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ScDeleteItemsResponse"/> class.
    /// </summary>
    /// <param name="itemsIds">List of item ids that were deleted.</param>
    public ScDeleteItemsResponse(List<string> itemsIds)
    {
      this.ItemsIds = itemsIds;
    }

    /// <summary>
    /// Returns number if items ids that were deleted.
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
    /// <param name="index">The index of item id.</param>
    ///
    /// <returns>
    ///     The  item GUID.
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
    ///      <see cref="IEnumerator{T}"/> that can be used to iterate through the item ids.
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
