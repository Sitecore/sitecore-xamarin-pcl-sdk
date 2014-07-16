using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;
  using SitecoreMobileSDKMockObjects;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.SessionSettings;



  [TestFixture]
  public class ThreadSafetyTest
  {
    [Test]
    public void TestSessionConfigCannotBeMutated()
    {
      var anonymous = new MutableSessionConfig("localhost", null, null);
      var defaultSource = ItemSource.DefaultSource();

      var session = new ScApiSession(anonymous, defaultSource);

      Assert.AreEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);

      anonymous.SetInstanceUrl("sitecore.net");
      anonymous.SetLogin("admin");
      anonymous.SetPassword("b");

      Assert.AreNotEqual(anonymous, session.Config);
      Assert.AreNotSame(anonymous, session.Config);
    }

    [Test]
    public void TestSessionDefaultSourceCannotBeMutated()
    {
      var anonymous = SessionConfig.NewAnonymousSessionConfig("localhost");
      var defaultSource = new MutableItemSource("master", "en");
      var session = new ScApiSession(anonymous, defaultSource);

      Assert.AreEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);



      defaultSource.SetDatabase("web");
      defaultSource.SetLanguage("da");
      defaultSource.SetVersion("100500");
      Assert.AreNotEqual(defaultSource, session.DefaultSource);
      Assert.AreNotSame(defaultSource, session.DefaultSource);
    }

    [Test]
    public void TestReadItemByIdCopiesSessionSettings()
    {
      var defaultSource = new MutableItemSource("master", "en", "33");
      var sessionSettings = new MutableSessionConfig("localhost", "user", "pass", "/sitecore/shell", "v100500");

      ScopeParameters scope = new ScopeParameters();
      scope.AddScope(ScopeType.Parent);
      scope.AddScope(ScopeType.Self);

      string[] fields = { "Ukraine", "is", "Europe" };
      var queryParameters = new QueryParameters(PayloadType.Content, scope, fields);


      ReadItemsByIdParameters request = new ReadItemsByIdParameters(sessionSettings, defaultSource, queryParameters, "{aaaa-aa-bb}");
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
      var defaultSource = new MutableItemSource("master", "en", "33");
      var sessionSettings = new MutableSessionConfig("localhost", "user", "pass", "/sitecore/shell", "v100500");

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
        defaultSource.SetVersion("1aa11");

        Assert.AreEqual("master", otherRequest.ItemSource.Database);
        Assert.AreEqual("en", otherRequest.ItemSource.Language);
        Assert.AreEqual("33", otherRequest.ItemSource.Version);

        Assert.AreNotSame(request.SessionSettings, otherRequest.SessionSettings);
        Assert.AreNotSame(request.ItemSource, otherRequest.ItemSource);
        Assert.AreNotSame(request.QueryParameters, otherRequest.QueryParameters);
      }
    }
  }
}

