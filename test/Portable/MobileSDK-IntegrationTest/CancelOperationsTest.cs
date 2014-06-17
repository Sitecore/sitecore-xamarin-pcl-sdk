namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;

  [TestFixture]
  public class CancelOperationsTest
  {
    private TestEnvironment testData;
    private ScApiSession session;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      this.session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
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
      var requestBuilder = new ReadItemByIdRequestBuilder(testData.Items.Home.Id);
      var request = requestBuilder.Build();
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
      var requestBuilder = new ReadItemByPathRequestBuilder(testData.Items.Home.Path);
      var request = requestBuilder.Build();
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
      var requestBuilder = new ReadItemByQueryRequestBuilder(testData.Items.Home.Path);
      var request = requestBuilder.Build();
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