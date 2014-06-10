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
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithId(testData.Items.Home.Id).Build();
      var cancelToken = CreateCancelTokenWithDelay(100);
      ScItemsResponse response = null;
      try
      {
        response = await session.ReadItemByIdAsync(request, cancelToken);
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
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithPath(testData.Items.Home.Path).Build();
      var cancelToken = CreateCancelTokenWithDelay(100);
      ScItemsResponse response = null;
      try
      {
        response = await session.ReadItemByPathAsync(request, cancelToken);
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
      var requestBuilder = new ItemWebApiRequestBuilder();
      var request = requestBuilder.RequestWithSitecoreQuery(testData.Items.Home.Path).Build();
      var cancelToken = CreateCancelTokenWithDelay(100);
      ScItemsResponse response = null;
      try
      {
        response = await session.ReadItemByQueryAsync(request, cancelToken);
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