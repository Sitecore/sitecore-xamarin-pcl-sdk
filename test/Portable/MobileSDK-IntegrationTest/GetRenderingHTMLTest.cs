﻿namespace MobileSDKIntegrationTest
{
  using System;
  using System.IO;
  using System.Threading.Tasks;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Exceptions;
  using Sitecore.MobileSDK.API.Request;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class GetRenderingHtmlTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiReadonlySession sessionAuthenticatedUser;

    private const string RenderingId = "{447AA0FC-95C8-4EFD-B64E-FBF880C42E2D}";
    private const string DatasourceId = "{44E7C4E6-764E-49ED-9850-9D1435E864CD}";

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      this.sessionAuthenticatedUser =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          .BuildReadonlySession();
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionAuthenticatedUser.Dispose();
      this.sessionAuthenticatedUser = null;
      this.testData = null;
    }

    [Test]
    public async void TestGetRendering()
    {
      var request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("{138C6674-A29C-4674-9666-F9126E34B99D}", "{133C5863-D020-4CDF-9AA3-4ED0015F2F30}")
        .Build();
      Stream stream = await this.sessionAuthenticatedUser.ReadRenderingHtmlAsync(request);
      string response = this.StreamToString(stream);
      const string Expected = "<div style=\"display: inline-block;\" xmlns:scmobile=\"http://www.sitecore.net/scmobile\"><script type=\"text/javascript\"> \r\n        function scmobile_send_email_ID0EHBEA()\r\n        {\r\n\t\t\r\n          function htmlDecode( str )\r\n          {\r\n\t\t    if ( str.length == 0 )\r\n\t\t\t   return '';\r\n            var div = document.createElement('div');\r\n            div.innerHTML  = str;\r\n            return div.firstChild.nodeValue;\r\n          }\r\n\r\n\t\t  var email = new scmobile.share.Email();\r\n\t\t  \r\n          var brTagStr_ = unescape('%3C%62%72%2F%3E');\r\n          email.toRecipients  = htmlDecode( '' ).split(brTagStr_);\r\n          email.ccRecipients  = htmlDecode( '' ).split(brTagStr_);\r\n          email.bccRecipients = htmlDecode( '' ).split(brTagStr_);\r\n          email.subject       = htmlDecode( '' );\r\n          \r\n\t\t  var localBody = '';\r\n\t\t  var loc = window.location;\r\n\t\t  var hostPrefix = loc.protocol + '//' + loc.hostname + ':' + loc.port;\r\n\t\t  \r\n\t\t  var fullMediaPrefix = '\"' + hostPrefix + '/~/';\r\n\t\t  var exists = false;\r\n\t\t  if (localBody.indexOf('\"/~/') > 0)\r\n\t\t\t\texists = true;\r\n\t\t  while (exists)\r\n\t\t  {\r\n\t\t\tlocalBody = localBody.replace('\"/~/', fullMediaPrefix);\r\n\r\n\t\t\tif (localBody.indexOf('\"/~/') > 0)\r\n\t\t\t\texists = true;\r\n\t\t\telse\r\n\t\t\t\texists = false;\r\n\t\t  }\r\n\t\t  email.messageBody = localBody;\r\n\t\t  \r\n          email.isHTML = true;\r\n\t\t  \r\n          function onSuccess( data )\r\n          {\r\n              scmobile.console.log('onSuccess: ' + data.result);\r\n          }\r\n\r\n          function onError( data )\r\n          {\r\n            scmobile.console.log('onError: ' + data.error);\r\n          }\r\n\r\n          email.send( onSuccess, onError );\r\n        }\r\n\t  </script><a onclick=\"scmobile_send_email_ID0EHBEA()\"><img src=\"/~/media/Mobile SDK/mobile_email.ashx?h=48&amp;la=en&amp;w=48\" alt=\"\" width=\"48\" height=\"48\" /></a></div>";

      Assert.AreEqual(Expected, response);
    }

    [Test]
    public async void TestGetRenderingFromMasterDbAndShellSite()
    {
      var adminSession = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
        .Credentials(this.testData.Users.Admin)
        .Site(this.testData.ShellSite)
        .BuildSession();
      var request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(DatasourceId, RenderingId)
        .SourceAndRenderingDatabase("master")
        .Build();
      var response = await this.GetStringResponse(adminSession, request);
      const string Expected = "<div><h1>Sitecore</h1><div><p>Welcome to Sitecore!</p></div><div><p> a: </p><p> b: </p></div></div>";

      Assert.AreEqual(Expected, response);
    }

    [Test]
    public async void TestGetRenderingFromWebDbAsAnonymous()
    {
      var anonymousSession = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(this.testData.InstanceUrl).BuildSession();
      var request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(DatasourceId, RenderingId)
        .SourceAndRenderingDatabase("web")
        .Build();
      var response = await this.GetStringResponse(anonymousSession, request);
      const string Expected = "<div><h1>Sitecore web</h1><div><p>Welcome to Sitecore!</p></div><div><p> a: </p><p> b: </p></div></div>";

      Assert.AreEqual(Expected, response);
    }

    [Test]
    public async void TestGetRenderingForDanishLanguageAnd1Version()
    {
      var request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(DatasourceId, RenderingId)
        .SourceAndRenderingDatabase("master")
        .SourceAndRenderingLanguage("da")
        .SourceVersion(1)
        .Build();
      var response = await this.GetStringResponse(this.sessionAuthenticatedUser, request);
      const string Expected = "<div><h1>Danish version 1 master</h1><div><p>میرا انجام HTML ورژن&nbsp;<br />\r\n我的渲染HTML版本</p></div><div><p> a: </p><p> b: </p></div></div>";

      Assert.AreEqual(Expected, response);
    }

    //Item Web API issue #421406
    [Ignore]
    [Test]
    public async void TestGetRenderingWithCustomRenderingParams()
    {
      var request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(DatasourceId, RenderingId)
        .SourceAndRenderingDatabase("master")
        .AddAdditionalParameterNameValue("a", "aaaa")
        .AddAdditionalParameterNameValue("b", "bbbb")
        .Build();
      var response = await this.GetStringResponse(this.sessionAuthenticatedUser, request);
      const string Expected = "<div><h1>Sitecore</h1><div><p>Welcome to Sitecore!</p></div><div><p> a: aaaa</p><p> b: bbbb</p></div></div>";

      Assert.AreEqual(Expected, response);
    }

    [Test]
    public void TestGetRenderingWithNullDatabaseReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() =>
        ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(DatasourceId, RenderingId)
          .SourceAndRenderingDatabase(null)
          .Build());
      Assert.IsTrue(exception.Message.Contains("sourceAndRenderingDatabase"));
    }

    [Test]
    public void TestGetRenderingWithNullItemVersionReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() =>
        ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(DatasourceId, RenderingId)
          .SourceVersion(null)
          .Build());
      Assert.IsTrue(exception.Message.Contains("sourceVersion"));
    }

    [Test]
    public void TestGetRenderingWithNullRenderingIdReturnsException()
    {
      var exception = Assert.Throws<ArgumentNullException>(() =>
        ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(DatasourceId, null)
          .Build());
      Assert.IsTrue(exception.Message.Contains("renderingId"));
    }

    [Test]
    public void TestGetRenderingWithEmptyRenderingIdReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() =>
        ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(DatasourceId, " ")
          .Build());
      Assert.AreEqual("RenderingHtmlRequestBuilder.renderingId : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestGetRenderingWithEmptySourceItemIdReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() =>
        ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("", RenderingId)
          .Build());
      Assert.AreEqual("RenderingHtmlRequestBuilder.sourceId : The input cannot be empty.", exception.Message);
    }

    [Test]
    public void TestGetRenderingWithInvalidSourceItemIdReturnsException()
    {
      var exception = Assert.Throws<ArgumentException>(() =>
        ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("}invalid ID", RenderingId)
          .Build());
      Assert.AreEqual("RenderingHtmlRequestBuilder.sourceId : Item id must have curly braces '{}'", exception.Message);
    }

    [Test]
    public void TestGetRenderingWithNotExistentSourceItemIdReturnsException()
    {
      var exception = Assert.Throws<LoadDataFromNetworkException>(() => ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("{447AA0FC-95C0-4EFD-B64E-0BF880C42E2D}", RenderingId)
        .SourceAndRenderingDatabase("master")
        .Build());
      Assert.AreEqual("[Sitecore Mobile SDK] Unable to download data from the internet", exception.Message);
    }

    [Test]
    public void TestGetRenderingWithNullLanguageReturnsException()
    {
      Exception exception = Assert.Throws<ArgumentNullException>(() =>
        ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(DatasourceId, RenderingId)
        .SourceAndRenderingLanguage(null)
          .Build());
      Assert.IsTrue(exception.Message.Contains("sourceAndRenderingLanguage"));
    }



    private async Task<string> GetStringResponse(ISitecoreWebApiReadonlySession session, IGetRenderingHtmlRequest request)
    {
      var stream = await session.ReadRenderingHtmlAsync(request);
      var response = this.StreamToString(stream);
      return response;
    }

    public string StreamToString(Stream stream)
    {
      stream.Position = 0;
      using (var reader = new StreamReader(stream))
      {
        return reader.ReadToEnd();
      }
    }
  }
}