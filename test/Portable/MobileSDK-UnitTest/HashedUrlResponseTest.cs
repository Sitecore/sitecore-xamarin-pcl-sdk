namespace Sitecore.MobileSdkUnitTest
{
  using System.Threading;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.CrudTasks.Resource;

  [TestFixture]
  public class HashedUrlResponseTest
  {
    [Test]
    public void TestValidResponseFromTheRealInstance()
    {
      string response = "{\"statusCode\":200,\"result\":\"http://cms75.test24dk1.dk.sitecore.net/~/media/images/green_mineraly1.ashx?thn=1\\u0026hash=0F741932010066E0442017C7E37A26AF2B55FCAA\"}";
      string result = HashedMediaUrlParser.Parse(response, default(CancellationToken));

      string expected = "http://cms75.test24dk1.dk.sitecore.net/~/media/images/green_mineraly1.ashx?thn=1&hash=0F741932010066E0442017C7E37A26AF2B55FCAA";
      Assert.AreEqual(expected, result);
    }
  

    [Test]
    public void TestErrorResponseCausesException()
    {
      string response =     
        "{\"statusCode\":401,\"error\":" +
        "{\"message\":\"Fake error message\"}" +
        "}";
      var ex = Assert.Throws<SSCJsonErrorException>( () =>  HashedMediaUrlParser.Parse(response, default(CancellationToken)) );

      Assert.AreEqual(401, ex.Response.StatusCode);
      Assert.AreEqual("Fake error message", ex.Response.Message);
    }


  }
}

