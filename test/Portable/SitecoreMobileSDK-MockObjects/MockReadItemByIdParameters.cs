﻿

namespace MobileSDKUnitTest.Mock
{
    using Sitecore.MobileSDK.API.Request;
    using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder;
  using Sitecore.MobileSDK.UrlBuilder.ItemById;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public class MockGetItemsByIdParameters : IReadItemsByIdRequest
  {
    public MockGetItemsByIdParameters()
    {
      this.CopyInvocationCount = 0;
    }

    public virtual IBaseGetItemRequest DeepCopyBaseGetItemRequest()
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

    public string ItemId { get; set; }

    public int CopyInvocationCount { get; private set; }
  }
}
