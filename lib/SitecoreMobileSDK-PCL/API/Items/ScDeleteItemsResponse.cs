namespace Sitecore.MobileSDK.API.Items
{
  using System.Collections.Generic;

  public class ScDeleteItemsResponse : IEnumerable<string>
  {
    private List<string> ItemsIds { get; set; }

    public ScDeleteItemsResponse(List<string> itemsIds)
    {
      this.ItemsIds = itemsIds;
    }

    public int Count 
    { 
      get
      {
        return this.ItemsIds.Count;
      }
    }

    #region IEnumerable
    public string this[int index]
    {
      get
      {
        return this.ItemsIds[index];
      }
    }

    public IEnumerator<string> GetEnumerator()
    {
      return this.ItemsIds.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.ItemsIds.GetEnumerator();
    }
    #endregion IEnumerable
  }
}
