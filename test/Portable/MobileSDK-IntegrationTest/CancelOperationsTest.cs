namespace MobileSDKIntegrationTest
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Threading;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.MockObjects;

  [TestFixture]
  public class CancelOperationsTest
  {
    private static readonly Action<string> DebugWriteLineBlock = message => Debug.WriteLine(message);

    private TestEnvironment testData;
    private ISitecoreSSCReadonlySession session;

    [SetUp]
    public void Setup()
    {
      using (new FunctionTracer("CancelOperationsTest->setup()", DebugWriteLineBlock))
      {
        this.testData = TestEnvironment.DefaultTestEnvironment();

        this.session =
          SitecoreSSCSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
            .Credentials(this.testData.Users.Admin)
            .BuildReadonlySession();
      }
    }

    [TearDown]
    public void TearDown()
    {
      using (new FunctionTracer("CancelOperationsTest->tearDown()", DebugWriteLineBlock))
      {
        this.testData = null;
        this.session.Dispose();
        this.session = null;
      }
    }

    [Test]
    public void TestCancelGetItemById()
    {
      using (new FunctionTracer("CancelOperationsTest->TestCancelGetItemById()", DebugWriteLineBlock))
      {
        var request = ItemSSCRequestBuilder.ReadItemsRequestWithId(this.testData.Items.Home.Id).Build();
        var cancelToken = CreateCancelTokenWithDelay(20);
        ScItemsResponse response = null;

        // @adk : do not use Task.WaitAll() since it may cause deadlocks
        TestDelegate testCode = async () =>
        {
          using (new FunctionTracer("session.ReadItemAsync()", DebugWriteLineBlock))
          {
            var task = this.session.ReadItemAsync(request, cancelToken);
            response = await task;
          }
        };
        var exception = Assert.Catch<OperationCanceledException>(testCode);
        Debug.WriteLine("Expected token : " + cancelToken);
        Debug.WriteLine("Received token : " + exception.CancellationToken);


        Assert.IsNull(response);
        //      Desktop (Windows) : "A task was canceled."
        //      iOS               : "The Task was canceled"
        Assert.IsTrue(exception.Message.ToLowerInvariant().Contains("task was canceled"));

        // @adk : CancellationToken class comparison or scheduling works differently on iOS
        // Assert.AreEqual(cancelToken, exception.CancellationToken);
      }
    }

    [Test]
    public void TestCancelGetItemByPath()
    {
      var request = ItemSSCRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Build();
      var cancelToken = CreateCancelTokenWithDelay(10);
      ScItemsResponse response = null;

      TestDelegate testCode = async () =>
      {
        var task = session.ReadItemAsync(request, cancelToken);
        response = await task;
      };
      var exception = Assert.Catch<OperationCanceledException>(testCode);

      Assert.IsNull(response);
      //      Desktop (Windows) : "A task was canceled."
      //      iOS               : "The Task was canceled"
      Assert.IsTrue(exception.Message.ToLowerInvariant().Contains("task was canceled"));

      // @adk : CancellationToken class comparison or scheduling works differently on iOS
      // Assert.AreEqual(cancelToken, exception.CancellationToken);
    }

    [Test]
    public void TestCancelGetMedia()
    {
      var request = ItemSSCRequestBuilder.DownloadResourceRequestWithMediaPath("/sitecore/media library/Images/test image").Build();
      var cancelToken = CreateCancelTokenWithDelay(5);
      Stream response = null;

      TestDelegate testCode = async () =>
      {
        var task = session.DownloadMediaResourceAsync(request, cancelToken);
        response = await task;
      };
      var exception = Assert.Catch<OperationCanceledException>(testCode);

      Assert.IsNull(response);
      //      Desktop (Windows) : "A task was canceled."
      //      iOS               : "The Task was canceled"
      Assert.IsTrue(exception.Message.ToLowerInvariant().Contains("task was canceled"));

      // @adk : CancellationToken class comparison or scheduling works differently on iOS
      // Assert.AreEqual(cancelToken, exception.CancellationToken);
    }

    private static CancellationToken CreateCancelTokenWithDelay(Int32 delay)
    {
      var cancelTokenSource = new CancellationTokenSource();
      cancelTokenSource.CancelAfter(delay);
      var cancelToken = cancelTokenSource.Token;

      return cancelToken;
    }

  }
}