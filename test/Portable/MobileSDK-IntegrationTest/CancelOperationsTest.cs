
namespace MobileSDKIntegrationTest
{
  using System;
  using System.IO;
  using System.Diagnostics;
  using System.Threading;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Session;

  using MobileSDKUnitTest.Mock;

  [TestFixture]
  public class CancelOperationsTest
  {
    private static Action<string> DebugWriteLineBlock  = (string message) =>
    {
      Debug.WriteLine(message);
    };

    private TestEnvironment                testData;
    private ISitecoreWebApiReadonlySession session ;

    [SetUp]
    public void Setup()
    {
      using ( FunctionTracer logger = new FunctionTracer("CancelOperationsTest->setup()", CancelOperationsTest.DebugWriteLineBlock) )
      {
        this.testData = TestEnvironment.DefaultTestEnvironment();

        this.session =
          SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
            .Credentials(this.testData.Users.Admin)
            .BuildReadonlySession();
      }
    }

    [TearDown]
    public void TearDown()
    {
      using (FunctionTracer logger = new FunctionTracer("CancelOperationsTest->tearDown()", CancelOperationsTest.DebugWriteLineBlock))
      {
        this.testData = null;
        this.session.Dispose();
        this.session = null;
      }
    }

    [Test]
    public void TestCancelGetItemById()
    {
      using (FunctionTracer logger = new FunctionTracer("CancelOperationsTest->TestCancelGetItemById()", CancelOperationsTest.DebugWriteLineBlock))
      {
        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Build();
        var cancelToken = CreateCancelTokenWithDelay(20);
        ScItemsResponse response = null;

        // @adk : do not use Task.WaitAll() since it may cause deadlocks
        TestDelegate testCode = async () =>
        {
          using (FunctionTracer sessionLogger = new FunctionTracer("session.ReadItemAsync()", CancelOperationsTest.DebugWriteLineBlock))
          {
            var task = session.ReadItemAsync(request, cancelToken);
            response = await task;
          }
        };
        var exception = Assert.Catch<OperationCanceledException>(testCode);
        Debug.WriteLine("Expected token : " + cancelToken.ToString());
        Debug.WriteLine("Received token : " + exception.CancellationToken.ToString());


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
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(testData.Items.Home.Path).Build();
      var cancelToken = CreateCancelTokenWithDelay(10);
      ScItemsResponse response = null;

      TestDelegate testCode = async() =>
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
      var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath("/sitecore/media library/Images/test image").Build();
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

    [Test]
    public void TestCancelGetItemByQuery()
    {
      var request = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(testData.Items.Home.Path).Build();
      var cancelToken = CreateCancelTokenWithDelay(10);
      ScItemsResponse response = null;

      TestDelegate testCode = async() =>
      {
        var task = session.ReadItemAsync(request, cancelToken);
        await task;
      };
      OperationCanceledException exception = Assert.Catch<OperationCanceledException>(testCode);

      Assert.IsNull(response);


      //      Desktop (Windows) : "A task was canceled."
      //      iOS               : "The Task was canceled"
      Assert.IsTrue(exception.Message.ToLowerInvariant().Contains("task was canceled"));


      // @adk : CancellationToken class comparison or scheduling works differently on iOS
      // Assert.AreEqual(cancelToken, exception.CancellationToken);
    }
  }
}