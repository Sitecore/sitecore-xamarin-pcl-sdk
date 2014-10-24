namespace Sitecore.MobileSDK.MockObjects
{
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public class MockGetItemsByQueryParameters : IReadItemsByQueryRequest
  {
    public MockGetItemsByQueryParameters()
    {
      this.CopyInvocationCount = 0;
    }

    public virtual IReadItemsByQueryRequest DeepCopyGetItemByQueryRequest()
    {
      ++this.CopyInvocationCount;

      MockGetItemsByQueryParameters result = new MockGetItemsByQueryParameters();
      result.SitecoreQuery = this.SitecoreQuery;

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

    public virtual IBaseItemRequest DeepCopyBaseGetItemRequest()
    {
      return this.DeepCopyGetItemByQueryRequest();
    }

    public IItemSource ItemSource { get; set; }

    public ISessionConfig SessionSettings { get; set; }

    public IQueryParameters QueryParameters { get; set; }

    public string SitecoreQuery { get; set; }

    public IPagingParameters PagingSettings { get; set; }

    public int CopyInvocationCount { get; private set; }
  }
}

