namespace Sitecore.MobileSDK.API.Items
{
  using System;
  using System.Collections.Generic;

  /// <summary>
  /// Class represents server response for read items request.
  /// </summary>
  public class ScItemsResponse : IEnumerable<ISitecoreItem>
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="ScItemsResponse"/> class.
    /// </summary>
    /// <param name="totalCount">Total items count in response</param>
    /// <param name="resultCount">Received items count in response</param>
    /// <param name="items">List of <see cref="ISitecoreItem"/></param>
    public ScItemsResponse(int totalCount, int resultCount, List<ISitecoreItem> items)
    {
      this.TotalCount = totalCount;
      this.ResultCount = resultCount;
      this.Items = items;
    }

    #region Paging
    /// <summary>
    /// Returns total count of items response.
    /// </summary>
    public int TotalCount { get; private set; }

    /// <summary>
    /// Returns items count received in response.
    /// </summary>
    public int ResultCount { get; private set; }
    #endregion Paging

    #region IEnumerable 
    /// <summary>
    ///     Returns an enumerator that iterates through the items list. Not generic version.
    /// </summary>
    /// <returns>
    ///      <see cref="IEnumerator{T}"/> that can be used to iterate through the items.
    /// </returns>
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.Items.GetEnumerator();
    }

    /// <summary>
    ///     Returns an enumerator that iterates through the items list.
    /// </summary>
    /// <returns>
    ///      <see cref="IEnumerator{T}"/> that can be used to iterate through the items.
    /// </returns>
    public IEnumerator<ISitecoreItem> GetEnumerator()
    {
      return this.Items.GetEnumerator();
    }

    /// <summary>
    ///     Gets the item that was received.
    /// </summary>
    /// <param name="index">The index of item.</param>
    ///
    /// <returns>
    ///     <see cref="ISitecoreItem"/>.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException"> index is less than 0 or index is equal to or greater than <see cref="List{T}.Count"/>.</exception>
    public ISitecoreItem this[int index]
    {
      get
      {
        return this.Items[index];
      }
    }

    private List<ISitecoreItem> Items
    {
      get;
      /*private*/
      set;
    }
    #endregion IEnumerable
  }
}