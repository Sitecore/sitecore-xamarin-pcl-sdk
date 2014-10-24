namespace Sitecore.MobileSDK.MockObjects
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class MockGetItemsByIdParameters : IReadItemsByIdRequest
  {
    public MockGetItemsByIdParameters()
    {
      this.CopyInvocationCount = 0;
    }

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetItemByIdRequest();
    }

    public virtual IReadItemsByIdRequest DeepCopyGetItemByIdRequest()
    {
      ++this.CopyInvocationCount;

      MockGetItemsByIdParameters result = new MockGetItemsByIdParameters();
      result.ItemId = this.ItemId;

      if (null != this.ItemSource)
      {
        result.ItemSource = this.ItemSource.ShallowCopy();
      }

      if (null != this.SessionSettings)
      {
        result.SessionSettings = this.SessionSettings.SessionConfigShallowCopy();
      }

      if (null == this.QueryParameters)
      {
        result.QueryParameters = this.QueryParameters.DeepCopy();
      }

      return result;
    }

    public IItemSource ItemSource { get; set; }

    public ISessionConfig SessionSettings { get; set; }

    public IQueryParameters QueryParameters { get; set; }

    public IPagingParameters PagingSettings { get; set; }

    public string ItemId { get; set; }

    public int CopyInvocationCount { get; private set; }
  }
}
