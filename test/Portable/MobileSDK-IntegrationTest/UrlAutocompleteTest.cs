namespace MobileSDKIntegrationTest
{
  using System.Threading.Tasks;
  using NUnit.Framework;

  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Session;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;


  [TestFixture]
  public class UrlAutocompleteTest
  {
    private TestEnvironment testData;

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
    }

    [TearDown]
    public void TearDown()
    {
      this.testData = null;
    }

    [Test]
    public async void TestWithoutHttpInUrlByPath()
    {
      var urlWithoutHttp = this.RemoveHttpSymbols(this.testData.InstanceUrl);
      var url = urlWithoutHttp;

      using
      (
        var session =
          SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(url)
            .Credentials(this.testData.Users.Admin)
            .BuildReadonlySession()
      )
      {
        var requestWithItemPath = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path).Payload(PayloadType.Content).Build();
        var response = await session.ReadItemAsync(requestWithItemPath);

        testData.AssertItemsCount(1, response);
        testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      }
    }

    [Test]
    public async void TestWithoutHttpInUrlAndWithTwoSlashInTheEndByPath()
    {
      var urlWithTwoSlash = testData.InstanceUrl + "//";
      var url = this.RemoveHttpSymbols(urlWithTwoSlash);

      using
      (
        var session =
          SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(url)
            .Credentials(this.testData.Users.Admin)
            .BuildReadonlySession()
      )
      {
        var requestWithItemPath = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.testData.Items.Home.Path).Payload(PayloadType.Content).Build();
        var response = await session.ReadItemAsync(requestWithItemPath);

        testData.AssertItemsCount(1, response);
        testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      }
    }

    [Test]
    public async void TestWithoutHttpInUrlByQuery()
    {
      var urlWithoutHttp = this.RemoveHttpSymbols(this.testData.InstanceUrl);
      var url = urlWithoutHttp;

      using
     (
       var session =
         SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(url)
           .Credentials(this.testData.Users.Admin)
           .BuildReadonlySession()
     )
      {
        var requestWithItemQuery = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(this.testData.Items.Home.Path).Payload(PayloadType.Content).Build();
        var response = await session.ReadItemAsync(requestWithItemQuery);

        testData.AssertItemsCount(1, response);
        testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      }
    }

    [Test]
    public async void TestWithHttpInUrlAndWithOneSlashInTheEndByQuery()
    {
      var urlWithOneSlash = testData.InstanceUrl + "/";
      var url = urlWithOneSlash;

      using
     (
       var session =
         SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(url)
           .Credentials(this.testData.Users.Admin)
           .BuildReadonlySession()
     )
      {
        var requestWithItemQuery = ItemWebApiRequestBuilder.ReadItemsRequestWithSitecoreQuery(this.testData.Items.Home.Path).Payload(PayloadType.Content).Build();
        var response = await session.ReadItemAsync(requestWithItemQuery);

        testData.AssertItemsCount(1, response);
        testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      }
    }

    [Test]
    public async void TestWithHttpsInUrlById()
    {
      var url = "https://scmobileteam.sitecoretest.net";

      // @adk : inlined due to 
      //
      // Failed UrlAutocompleteTest.TestWithHttpsInUrlById
      //
      // MESSAGE:
      // System.Threading.Tasks.TaskCanceledException : A task was canceled.
      // +++++++++++++++++++
      // STACK TRACE:
      //    at Microsoft.Runtime.CompilerServices.TaskAwaiter.ThrowForNonSuccess(Task task)
      //    at Microsoft.Runtime.CompilerServices.TaskAwaiter.HandleNonSuccess(Task task)
      //    at Sitecore.MobileSDK.ScApiSession.<ReadItemAsync>d__a.MoveNext() in c:\dev\Jenkins\jobs\XamarinSDK-FullBuild\workspace\lib\SitecoreMobileSDK-PCL\ScApiSession.cs:line 215
      using
      (
        var session = 
          SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(url)
            .Credentials(this.testData.Users.Admin)
            .BuildReadonlySession()
      )
      {
        var requestWithItemId = ItemWebApiRequestBuilder.ReadItemsRequestWithId(this.testData.Items.Home.Id).Payload(PayloadType.Content).Build();
        var response = await session.ReadItemAsync(requestWithItemId);

        testData.AssertItemsCount(1, response);
        testData.AssertItemsAreEqual(testData.Items.Home, response[0]);
      }
    }

    private string RemoveHttpSymbols(string url)
    {
      return url.Remove(0, 7);
    }
  }
}
