

namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;

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
    public void TestCancelGetItemById()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Build();
      var cancelToken = CreateCancelTokenWithDelay(20);
      ScItemsResponse response = null;

      TestDelegate testCode = () =>
      {
        var task = session.ReadItemAsync(request, cancelToken);
        Task.WaitAll(task);
      };
      OperationCanceledException exception = Assert.Throws<OperationCanceledException>(testCode);

      Assert.IsNull(response);
      Assert.AreEqual(cancelToken, exception.CancellationToken);
      Assert.AreEqual("A task was canceled.", exception.Message);
    }

    [Test]
    public void TestCancelGetItemByPath()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Build();
      var cancelToken = CreateCancelTokenWithDelay(10);
      ScItemsResponse response = null;


      TestDelegate testCode = () =>
      {
        var task = session.ReadItemAsync(request, cancelToken);
        Task.WaitAll(task);
      };
      OperationCanceledException exception = Assert.Throws<OperationCanceledException>(testCode);

      Assert.IsNull(response);
      Assert.AreEqual(cancelToken, exception.CancellationToken);
      Assert.AreEqual("A task was canceled.", exception.Message);
    }

    private static CancellationToken CreateCancelTokenWithDelay(Int32 delay)
    {
      var cancelTokenSource = new CancellationTokenSource();
      cancelTokenSource.CancelAfter(delay);
      var cancelToken = cancelTokenSource.Token;

      return cancelToken;
    }

    [Test]
    public void TestCancelGetItemByQuery()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Path).Build();
      var cancelToken = CreateCancelTokenWithDelay(10);
      ScItemsResponse response = null;

      TestDelegate testCode = () =>
      {
        var task = session.ReadItemAsync(request, cancelToken);
        Task.WaitAll(task);
      };
      OperationCanceledException exception = Assert.Throws<OperationCanceledException>(testCode);

      Assert.IsNull(response);
      Assert.AreEqual(cancelToken, exception.CancellationToken);
      Assert.AreEqual("A task was canceled.", exception.Message);
    }
  }
}