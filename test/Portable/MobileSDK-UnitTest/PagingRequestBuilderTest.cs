namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request.Parameters;


  [TestFixture]
  public class PagingRequestBuilderTest
  {
    [Test]
    public void TestPagingSettingsCanBeReordered()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dynamo}")
        .AddScope(ScopeType.Children)
        .PageNumber(2)
        .ItemsPerPage(5)
        .Build();

      Assert.IsTrue(request.QueryParameters.ScopeParameters.ChildrenScopeIsSet);
      Assert.IsFalse(request.QueryParameters.ScopeParameters.ParentScopeIsSet);
      Assert.IsFalse(request.QueryParameters.ScopeParameters.SelfScopeIsSet);

      Assert.AreEqual(2, request.PagingSettings.PageNumber);
      Assert.AreEqual(5, request.PagingSettings.ItemsPerPageCount);
    }

    [Test]
    public void TestPagingSettingsUsedForSelfScopeAndSingleItemResponse()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{karpaty}")
        .AddScope(ScopeType.Self)
        .PageNumber(5)
        .ItemsPerPage(10)
        .Build();

      Assert.IsFalse(request.QueryParameters.ScopeParameters.ChildrenScopeIsSet);
      Assert.IsFalse(request.QueryParameters.ScopeParameters.ParentScopeIsSet);
      Assert.IsTrue(request.QueryParameters.ScopeParameters.SelfScopeIsSet);

      Assert.AreEqual(5, request.PagingSettings.PageNumber);
      Assert.AreEqual(10, request.PagingSettings.ItemsPerPageCount);
    }

    [Test]
    public void TestPagingSettingsUsedForOmittedScopeAndSingleItemResponse()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{karpaty}")
        .PageNumber(44)
        .ItemsPerPage(5)
        .Build();

      Assert.AreEqual(44, request.PagingSettings.PageNumber);
      Assert.AreEqual(5, request.PagingSettings.ItemsPerPageCount);
    }
  }
}

