namespace Sitecore.MobileSDK.API.Items
{
  using System.Collections;
  using System.Collections.Generic;

  public class ScItemsResponse : IEnumerable<ISitecoreItem>
  {
    public ScItemsResponse(int totalCount, int resultCount, List<ISitecoreItem> items)
    {
      this.TotalCount = totalCount;
      this.ResultCount = resultCount;
      this.Items = items;
    }

    #region Paging
    public int TotalCount { get; private set; }

    public int ResultCount { get; private set; }
    #endregion Paging

    #region IEnumerable
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
      return this.Items.GetEnumerator();
    }

    public IEnumerator<ISitecoreItem> GetEnumerator()
    {
      return this.Items.GetEnumerator() as IEnumerator<ISitecoreItem>;
    }

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
      /*private*/ set; 
    }
    #endregion IEnumerable
  }
}