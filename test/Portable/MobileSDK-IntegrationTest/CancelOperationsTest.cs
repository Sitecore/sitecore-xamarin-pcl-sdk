namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;

  [TestFixture]
  public class CancelOperationsTest
  {
    private TestEnvironment testData;
    private ScApiSession session;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      var config = new SessionConfig(testData.AuthenticatedInstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      this.session = new ScApiSession(config, ItemSource.DefaultSource());
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
      this.session = null;
    }

    [Test]
    public async void TestCancelGetItemById()
    {
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Build();
      var cancelToken = CreateCancelTokenWithDelay(20);
      ScItemsResponse response = null;
      try
      {
        response = await session.ReadItemAsync(request, cancelToken);
      }
      catch (OperationCanceledException exception)
      {
        Assert.IsNull(response);
        Assert.AreEqual(cancelToken, exception.CancellationToken);
        Assert.AreEqual("A task was canceled.", exception.Message);
        return;
      }
      Assert.Fail("Operation should be cancelled");
    }

    [Test]
    public async void TestCancelGetItemByPath()
    {
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Build();
      var cancelToken = CreateCancelTokenWithDelay(10);
      ScItemsResponse response = null;
      try
      {
        response = await session.ReadItemAsync(request, cancelToken);
      }
      catch (OperationCanceledException exception)
      {
        Assert.IsNull(response);
        Assert.AreEqual(cancelToken, exception.CancellationToken);
        Assert.AreEqual("A task was canceled.", exception.Message);
        return;
      }
      Assert.Fail("Operation should be cancelled");
    }

    private static CancellationToken CreateCancelTokenWithDelay(Int32 delay)
    {
      var cancelTokenSource = new CancellationTokenSource();
      cancelTokenSource.CancelAfter(delay);
      var cancelToken = cancelTokenSource.Token;
      return cancelToken;
    }

    [Test]
    public async void TestCancelGetItemByQuery()
    {
      
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Path).Build();
      var cancelToken = CreateCancelTokenWithDelay(10);
      ScItemsResponse response = null;
      try
      {
        response = await session.ReadItemAsync(request, cancelToken);
      }
      catch (OperationCanceledException exception)
      {
        Assert.IsNull(response);
        Assert.AreEqual(cancelToken, exception.CancellationToken);
        Assert.AreEqual("A task was canceled.", exception.Message);
        return;
      }
      Assert.Fail("Operation should be cancelled");
    }
  }
}