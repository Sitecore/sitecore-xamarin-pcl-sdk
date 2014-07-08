namespace Sitecore.MobileSdkUnitTest
{
  using NUnit.Framework;
  using System;
  using System.Threading;
  using System.Threading.Tasks;
  using UnitTest_Desktop_NUnitLite;

  [TestFixture()]
  public class AuthenticateResponseParserTest
  {
    [Test]
    public void TestParseNullData()
    {
      TestDelegate action = () => AuthenticateResponseParser.ParseResponse(null, CancellationToken.None);
      Assert.Throws<ArgumentException>(action, "cannot parse null response");
    }

    [Test]
    public void TestParseEmptyData()
    {
      var response = "";
      TestDelegate action = () => AuthenticateResponseParser.ParseResponse(response, CancellationToken.None);

      Assert.Throws<ArgumentException>(action, "cannot parse null response");
    }

    [Test]
    public void TestParseCorrectData()
    {
      var response = "{\"statusCode\":200,\"result\":{}}";
      Boolean isValid = AuthenticateResponseParser.ParseResponse(response, CancellationToken.None);

      Assert.True(isValid);
    }

    public void TestParseBrokenData()
    {
      var response = "{}";
			Boolean isValid = AuthenticateResponseParser.ParseResponse (response, CancellationToken.None);

			Assert.False (isValid);
		}

    [Test]
    public void TestParseDeniedResponseData()
    {
      var response = "{\"statusCode\":401,\"error\":{\"message\":\"Access to site is not granted.\"}}";
      Boolean isValid = AuthenticateResponseParser.ParseResponse(response, CancellationToken.None);

      Assert.False(isValid);
    }

    [Test]
    public void TestCancellationCausesOperationCanceledException()
    {
      TestDelegate testAction = async () =>
      {
        var cancel = new CancellationTokenSource();

        Task<Boolean> action = Task.Factory.StartNew(() =>
        {
          var millisecondTimeout = 10;
          Thread.Sleep(millisecondTimeout);

          var response = "{\"statusCode\":200,\"result\":{}}";

          return AuthenticateResponseParser.ParseResponse(response, cancel.Token);
        });

        cancel.Cancel();
        await action;
      };

      Assert.Catch<OperationCanceledException>(testAction);
    }
  }
}

