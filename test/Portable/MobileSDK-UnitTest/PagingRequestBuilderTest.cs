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
      var first = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dynamo}")
        .AddScope(ScopeType.Children)
        .PageNumber(2)
        .ItemsPerPage(5)
        .Build();

      var second = ItemWebApiRequestBuilder.ReadItemsRequestWithId("{dynamo}")
        .ItemsPerPage(5)
        .PageNumber(2)
        .AddScope(ScopeType.Children)
        .Build();

      {
        Assert.IsTrue(first.QueryParameters.ScopeParameters.ChildrenScopeIsSet);
        Assert.IsFalse(first.QueryParameters.ScopeParameters.ParentScopeIsSet);
        Assert.IsFalse(first.QueryParameters.ScopeParameters.SelfScopeIsSet);

        Assert.AreEqual(2, first.PagingSettings.PageNumber);
        Assert.AreEqual(5, first.PagingSettings.ItemsPerPageCount);
      }

      {
        Assert.AreEqual(first.QueryParameters.ScopeParameters.ChildrenScopeIsSet, second.QueryParameters.ScopeParameters.ChildrenScopeIsSet);
        Assert.AreEqual(first.QueryParameters.ScopeParameters.ParentScopeIsSet, second.QueryParameters.ScopeParameters.ParentScopeIsSet);
        Assert.AreEqual(first.QueryParameters.ScopeParameters.SelfScopeIsSet, second.QueryParameters.ScopeParameters.SelfScopeIsSet);

        Assert.AreEqual(first.PagingSettings.PageNumber, second.PagingSettings.PageNumber);
        Assert.AreEqual(first.PagingSettings.ItemsPerPageCount, second.PagingSettings.ItemsPerPageCount);
      }
    }

    [Test]
    public void TestPagingSettingsUsedForSelfScopeAndSingleItemResponse()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/A/B/C/D/E")
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
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery("/x/y/z/*")
        .PageNumber(44)
        .ItemsPerPage(5)
        .Build();

      Assert.AreEqual(44, request.PagingSettings.PageNumber);
      Assert.AreEqual(5, request.PagingSettings.ItemsPerPageCount);
    }
  }
}

