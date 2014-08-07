namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.Items;

  [TestFixture]
  public class DeleteItemsResponseParserTest
  {
    [Test]
    public void TestParseNullData()
    {
      TestDelegate action = () => DeleteItemsResponseParser.ParseResponse(null, CancellationToken.None);
      Assert.Throws<ArgumentException>(action, "cannot parse null response");
    }

    [Test]
    public void TestParseEmptyData()
    {
      var responseString = "";
      TestDelegate action = () => DeleteItemsResponseParser.ParseResponse(responseString, CancellationToken.None);

      Assert.Throws<ArgumentException>(action, "cannot parse empty response");
    }

    [Test]
    public void TestParseSingleItemCorrectData()
    {
      var responseString = "{\"statusCode\":200,\"result\":{\"count\":1,\"itemIds\":" +
                           "[\"{40C28707-DCB1-4614-842A-8BE17880921E}\"]}}";
      ScDeleteItemsResponse response = DeleteItemsResponseParser.ParseResponse(responseString, CancellationToken.None);

      Assert.AreEqual(1, response.Count);
    }

    [Test]
    public void TestParseMultiItemCorrectData()
    {
      var responseString = "{\"statusCode\":200,\"result\":{\"count\":5,\"itemIds\":[\"" +
                     "{B0ED4777-1F5D-478D-AF47-145CCA9E4311}\"," +
                     "\"{FDB51F93-47ED-4186-AED8-FCAB82DA5BE7}\"," +
                     "\"{F27C2E36-7907-41F3-ADB3-0118220E4DF8}\"," +
                     "\"{9691126E-6BB9-47C5-9AA9-6EEAC9D4E70B}\"," +
                     "\"{97154E04-622E-4561-A20C-B6463AEB2AEE}\"]}}";
      ScDeleteItemsResponse response = DeleteItemsResponseParser.ParseResponse(responseString, CancellationToken.None);

      Assert.AreEqual(5, response.Count);
      Assert.AreEqual("{B0ED4777-1F5D-478D-AF47-145CCA9E4311}", response.ItemsIds[0]);
      Assert.AreEqual("{FDB51F93-47ED-4186-AED8-FCAB82DA5BE7}", response.ItemsIds[1]);
      Assert.AreEqual("{F27C2E36-7907-41F3-ADB3-0118220E4DF8}", response.ItemsIds[2]);
      Assert.AreEqual("{9691126E-6BB9-47C5-9AA9-6EEAC9D4E70B}", response.ItemsIds[3]);
      Assert.AreEqual("{97154E04-622E-4561-A20C-B6463AEB2AEE}", response.ItemsIds[4]);
    }

    [Test]
    public void TestErrorResponseData()
    {
      TestDelegate action = () =>
      {
        var responseString = "{\"statusCode\":401,\"error\":" +
                             "{\"message\":\"Access to the \u0027master\u0027 database is denied. " +
                             "Only members of the Sitecore Client Users role can switch databases.\"}" +
                             "}";
        DeleteItemsResponseParser.ParseResponse(responseString, CancellationToken.None);
      };

      var exception = Assert.Throws<WebApiJsonErrorException>(action);

      Assert.AreEqual(401, exception.Response.StatusCode);
      Assert.AreEqual("Access to the \u0027master\u0027 database is denied." +
                      " Only members of the Sitecore Client Users role can switch databases.",
        exception.Response.Message);
    }

    [Test]
    public void TestEmptyItemsResponseData()
    {
      var responseString = "{\"statusCode\":200,\"result\":{\"count\":0,\"itemIds\":[]}}";
      ScDeleteItemsResponse response = DeleteItemsResponseParser.ParseResponse(responseString, CancellationToken.None);

      Assert.AreEqual(0, response.Count);
    }

    [Test]
    public void TestCancellationCausesOperationCanceledException()
    {
      TestDelegate testAction = async () =>
      {
        var cancel = new CancellationTokenSource();

        Task<ScDeleteItemsResponse> action = Task.Factory.StartNew(() =>
        {
          var millisecondTimeout = 10;
          Thread.Sleep(millisecondTimeout);

          var responseString = "{\"statusCode\":200,\"result\":{\"count\":1,\"itemIds\":" +
                               "[\"{40C28707-DCB1-4614-842A-8BE17880921E}\"]}}";
          return DeleteItemsResponseParser.ParseResponse(responseString, cancel.Token);
        });

        cancel.Cancel();
        await action;
      };

      Assert.Catch<OperationCanceledException>(testAction);
    }
  }
}

