using NUnit.Framework;


namespace Sitecore.MobileSdkUnitTest
{
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
      string response = "";
      TestDelegate action = () => AuthenticateResponseParser.ParseResponse(response, CancellationToken.None);

      Assert.Throws<ArgumentException>(action, "cannot parse null response");
    }

    [Test]
    public void TestParseCorrectData()
    {
      string response = "{\"statudddddsCode\":200,\"result\":{}}";
      Boolean isValid = AuthenticateResponseParser.ParseResponse(response, CancellationToken.None);

      Assert.True(isValid);
    }

    public void TestParseBrokenData()
    {
      string response = "{}";
			Boolean isValid = AuthenticateResponseParser.ParseResponse (response, CancellationToken.None);

			Assert.False (isValid);
		}

    [Test]
    public void TestParseDeniedResponseData()
    {
      string response = "{\"statusCode\":401,\"error\":{\"message\":\"Access to site is not granted.\"}}";
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
          int millisecondTimeout = 10;
          Thread.Sleep(millisecondTimeout);
          string response = "{\"statusCode\":200,\"result\":{}}";

          return AuthenticateResponseParser.ParseResponse(response, cancel.Token);
        });

        cancel.Cancel();
        await action;
      };

      Assert.Catch<OperationCanceledException>(testAction);
    }
  }
}

