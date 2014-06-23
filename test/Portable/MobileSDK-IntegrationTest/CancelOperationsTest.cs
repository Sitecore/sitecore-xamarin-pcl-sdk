﻿using System.Diagnostics;
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
        OperationCanceledException exception = Assert.Catch<OperationCanceledException>(testCode);
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