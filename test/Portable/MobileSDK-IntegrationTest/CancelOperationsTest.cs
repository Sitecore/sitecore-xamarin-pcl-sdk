using System.Diagnostics;
using MobileSDKUnitTest.Mock;

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
    private static Action<string> DebugWriteLineBlock  = (string message) =>
    {
      Debug.WriteLine(message);
    };

    private TestEnvironment testData;
    private ScApiSession session;

    [SetUp]
    public void Setup()
    {
      using ( FunctionTracer logger = new FunctionTracer("CancelOperationsTest->setup()", CancelOperationsTest.DebugWriteLineBlock) )
      {
        this.testData = TestEnvironment.DefaultTestEnvironment();
        this.session = testData.GetSession(testData.InstanceUrl, testData.Users.Admin.Username, testData.Users.Admin.Password);
      }
    }

    [TearDown]
    public void TearDown()
    {
      using (FunctionTracer logger = new FunctionTracer("CancelOperationsTest->tearDown()", CancelOperationsTest.DebugWriteLineBlock))
      {
        this.testData = null;
        this.session = null;
      }
    }

    [Test]
    public void TestCancelGetItemById()
    {
      using (FunctionTracer logger = new FunctionTracer("CancelOperationsTest->TestCancelGetItemById()", CancelOperationsTest.DebugWriteLineBlock))
      {
        var cancelTokenSource = new CancellationTokenSource();
        var cancelToken = cancelTokenSource.Token;


        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(testData.Items.Home.Id).Build();
//        var cancelToken = CreateCancelTokenWithDelay(20);
        ScItemsResponse response = null;

        // @adk : do not fix async related warnings if any
        TestDelegate testCode = async () =>
        {
          using (FunctionTracer sessionLogger = new FunctionTracer("session.ReadItemAsync()", CancelOperationsTest.DebugWriteLineBlock))
          {
            var task = session.ReadItemAsync(request, cancelToken);
            cancelTokenSource.CancelAfter(20);

            response = await task;
          }
        };
        OperationCanceledException exception = Assert.Catch<OperationCanceledException>(testCode);
        Debug.WriteLine("Expected token : " + cancelToken.ToString());
        Debug.WriteLine("Received token : " + exception.CancellationToken.ToString());


        Assert.IsNull(response);

        Assert.AreEqual(cancelToken, exception.CancellationToken);


        Assert.AreEqual("A task was canceled.", exception.Message);
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
        await task;
      };
      OperationCanceledException exception = Assert.Catch<OperationCanceledException>(testCode);

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

      TestDelegate testCode = async() =>
      {
        var task = session.ReadItemAsync(request, cancelToken);
        await task;
      };
      OperationCanceledException exception = Assert.Catch<OperationCanceledException>(testCode);

      Assert.IsNull(response);
      Assert.AreEqual(cancelToken, exception.CancellationToken);
      Assert.AreEqual("A task was canceled.", exception.Message);
    }
  }
}