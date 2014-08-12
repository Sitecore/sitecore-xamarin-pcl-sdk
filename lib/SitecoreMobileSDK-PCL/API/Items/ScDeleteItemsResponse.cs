namespace Sitecore.MobileSDK.API.Items
{
  using System.Collections.Generic;

  public class ScDeleteItemsResponse
  {
    public int Count 
    { 
      get
      {
        return this.ItemsIds.Count;
      }
    }

    public List<string> ItemsIds { get; private set; }

    public ScDeleteItemsResponse(int count, List<string> itemsIds)
    {
//      this.Count = count;
      this.ItemsIds = itemsIds;
    }
  }
}
