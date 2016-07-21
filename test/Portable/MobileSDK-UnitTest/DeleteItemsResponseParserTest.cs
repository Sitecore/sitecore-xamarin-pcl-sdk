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
      var responseString = "204";
      ScDeleteItemsResponse response = DeleteItemsResponseParser.ParseResponse(responseString, CancellationToken.None);

      Assert.AreEqual(204, response.StatusCode);
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

      var exception = Assert.Throws<SSCJsonErrorException>(action);

      Assert.AreEqual(401, exception.Response.StatusCode);
      Assert.AreEqual("Access to the \u0027master\u0027 database is denied." +
                      " Only members of the Sitecore Client Users role can switch databases.",
        exception.Response.Message);
    }

    [Test]
    public void TestEmptyItemsResponseData()
    {
      var responseString = "444";
      ScDeleteItemsResponse response = DeleteItemsResponseParser.ParseResponse(responseString, CancellationToken.None);

      Assert.IsFalse(response.Deleted);
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

