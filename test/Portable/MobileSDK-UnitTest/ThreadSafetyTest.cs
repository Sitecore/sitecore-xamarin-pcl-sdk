namespace Sitecore.MobileSdkUnitTest
{
  using NUnit.Framework;

  using MobileSDKUnitTest.Mock;
  using SitecoreMobileSDKMockObjects;

  using Sitecore.MobileSDK.API.Request.Parameters;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


  [TestFixture]
  public class ThreadSafetyTest
  {
    MediaLibrarySettings mediaSettings;

    [SetUp]
    public void SetUp()
    {
      this.mediaSettings = new MediaLibrarySettings(
        "/sitecore/media library",
        "ashx",
        "~/media/");
    }

    [TearDown]
    public void TearDown()
    {
      this.mediaSettings = null;
    }


    [Test]
    public void TestSessionConfigCannotBeMutated()
    {
      var connection = new MutableSessionConfig("localhost");
      var anonymous = new MutableWebApiCredentialsPOD(null, null);

      var defaultSource = LegacyConstants.DefaultSource();

      var session = new ScApiSession(connection, anonymous, this.mediaSettings, defaultSource);

      Assert.AreEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);

      connection.SetInstanceUrl("sitecore.net");
      connection.SetSite("/sitecore/shell");
      connection.SetItemWebApiVersion("v100500");

      anonymous.Username = "admin";
      anonymous.Password = "b";

      Assert.AreNotEqual(connection, session.Config);
      Assert.AreNotSame(connection, session.Config);
    }

    [Test]
    public void TestCredentialsCannotBeMutated()
    {
      var connection = new MutableSessionConfig("localhost");
      var anonymous = new MutableWebApiCredentialsPOD(null, null);

      var defaultSource = LegacyConstants.DefaultSource();

      var session = new ScApiSession(connection, anonymous, this.mediaSettings, defaultSource);

      Assert.AreEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);

      anonymous.Username = "admin";
      anonymous.Password = "b";

      Assert.AreNotEqual(anonymous, session.Credentials);
      Assert.AreNotSame(anonymous, session.Credentials);
    }

    [Test]
    public void TestSessionDefaultSourceCannotBeMutated()
    {
      var connection = new SessionConfig("localhost");

      var defaultSource = new MutableItemSource("master", "en");
      var session = new ScApiSession(connection, null, this.mediaSettings, defaultSource);

      Assert.AreEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);



      defaultSource.SetDatabase("web");
      defaultSource.SetLanguage("da");
      defaultSource.SetVersion(100500);
      Assert.AreNotEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);
    }

    [Test]
    public void TestReadItemByIdCopiesSessionSettings()
    {
      var defaultSource = new MutableItemSource("master", "en", 33);
      var sessionSettings = new MutableSessionConfig("localhost", "/sitecore/shell", "v100500");

      ScopeParameters scope = new ScopeParameters();
      scope.AddScope(ScopeType.Parent);
      scope.AddScope(ScopeType.Self);

      string[] fields = { "Ukraine", "is", "Europe" };
      var queryParameters = new QueryParameters(PayloadType.Content, scope, fields);


      ReadItemsByIdParameters request = new ReadItemsByIdParameters(
        sessionSettings, 
        defaultSource, 
        queryParameters, 
        "{aaaa-aa-bb}");
      var otherRequest = request.DeepCopyGetItemByIdRequest();

      {
        sessionSettings.SetInstanceUrl("paappaa");

        Assert.AreEqual("localhost", otherRequest.SessionSettings.InstanceUrl);
        Assert.AreNotSame(request.SessionSettings, otherRequest.SessionSettings);
        Assert.AreNotSame(request.ItemSource, otherRequest.ItemSource);
        Assert.AreNotSame(request.QueryParameters, otherRequest.QueryParameters);
      }
    }

    [Test]
    public void TestReadItemByIdCopiesItemSource()
    {
      var defaultSource = new MutableItemSource("master", "en", 33);
      var sessionSettings = new MutableSessionConfig("localhost", "/sitecore/shell", "v100500");

      ScopeParameters scope = new ScopeParameters();
      scope.AddScope(ScopeType.Parent);
      scope.AddScope(ScopeType.Self);

      string[] fields = { "Ukraine", "is", "Europe" };
      var queryParameters = new QueryParameters(PayloadType.Content, scope, fields);


      ReadItemsByIdParameters request = new ReadItemsByIdParameters(sessionSettings, defaultSource, queryParameters, "{aaaa-aa-bb}");
      var otherRequest = request.DeepCopyGetItemByIdRequest();

      {
        defaultSource.SetDatabase("web");
        defaultSource.SetLanguage("xyz");
        defaultSource.SetVersion(9999);

        Assert.AreEqual("master", otherRequest.ItemSource.Database);
        Assert.AreEqual("en", otherRequest.ItemSource.Language);
        Assert.AreEqual(33, otherRequest.ItemSource.VersionNumber.Value);

        Assert.AreNotSame(request.SessionSettings, otherRequest.SessionSettings);
        Assert.AreNotSame(request.ItemSource, otherRequest.ItemSource);
        Assert.AreNotSame(request.QueryParameters, otherRequest.QueryParameters);
      }
    }
  }
}

