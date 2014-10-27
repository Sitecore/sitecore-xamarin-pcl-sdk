namespace MobileSDK_UnitTest_Desktop
{
  using NUnit.Framework;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.UrlBuilder;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.WebApi;

  [TestFixture]
  public class QueryParametersUrlBuilderTest
  {
    private QueryParametersUrlBuilder builder;

    [SetUp]
    public void SetUp()
    {
      IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
      IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();

      this.builder = new QueryParametersUrlBuilder(restGrammar, webApiGrammar);
    }

    [TearDown]
    public void TearDown()
    {
      this.builder = null;
    }

    [Test]
    public void TestQueryParametersWithNullFieldsProduceNullString()
    {
      string result = this.builder.BuildUrlString(new QueryParameters(null, null, null));
      Assert.AreEqual(string.Empty, result);
    }

    [Test]
    public void TestBuildValidQueryParams()
    {
      string result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Content, null, null));
      Assert.AreEqual("payload=content", result);

      result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Full, null, null));
      Assert.AreEqual("payload=full", result);

      result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Min, null, null));
      Assert.AreEqual("payload=min", result);

      result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Default, null, null));
      Assert.AreEqual("payload=min", result);
    }

    [Test]
    public void TestBuildValidScopeParams()
    {
      var scope = new ScopeParameters();
      scope.AddScope(ScopeType.Parent);
      var qp = new QueryParameters(null, scope, null);
      string result = this.builder.BuildUrlString(qp);
      Assert.AreEqual("scope=p", result);

      scope = new ScopeParameters();
      scope.AddScope(ScopeType.Self);
      qp = new QueryParameters(null, scope, null);
      result = this.builder.BuildUrlString(qp);
      Assert.AreEqual("scope=s", result);

      scope = new ScopeParameters();
      scope.AddScope(ScopeType.Children);
      qp = new QueryParameters(null, scope, null);
      result = this.builder.BuildUrlString(qp);
      Assert.AreEqual("scope=c", result);

      scope = new ScopeParameters();
      scope.AddScope(ScopeType.Parent);
      scope.AddScope(ScopeType.Self);
      scope.AddScope(ScopeType.Children);
      qp = new QueryParameters(null, scope, null);
      result = this.builder.BuildUrlString(qp);
      Assert.AreEqual("scope=p|s|c", result);
    }

    [Test]
    public void TestScopeParamsOrderIsPreserved()
    {
      var scope = new ScopeParameters();
      scope.AddScope(ScopeType.Children);
      scope.AddScope(ScopeType.Self);
      scope.AddScope(ScopeType.Parent);
      var qp = new QueryParameters(null, scope, null);
      string result = this.builder.BuildUrlString(qp);
      Assert.AreEqual("scope=c|s|p", result);
    }

    [Test]
    public void TestScopeParamsCanNotBeSetTwice()
    {
      var scope = new ScopeParameters();
      scope.AddScope(ScopeType.Children);
      scope.AddScope(ScopeType.Self);

      Assert.IsFalse(scope.AddScope(ScopeType.Children));
    }

    [Test]
    public void TestNullQueryParamsProduceNullString()
    {
      string result = this.builder.BuildUrlString(null);
      Assert.IsNull(result);
    }

    [Test]
    public void TestSimpleFieldListOfNamesIsProcessedCorrectly()
    {
      string[] fields = { "abra", "shwabra", "kadabra" };

      string result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Full, null, fields));
      Assert.AreEqual("payload=full&fields=abra|shwabra|kadabra", result);
    }


    [Test]
    public void TestFieldListOfNamesIsUrlEncodedCorrectly()
    {
      string[] fields = { "Слава Україні!", "Героям слава!" };

      string result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Content, null, fields));
      string expected = "payload=content&fields=%d0%a1%d0%bb%d0%b0%d0%b2%d0%b0%20%d0%a3%d0%ba%d1%80%d0%b0%d1%97%d0%bd%d1%96%21|%d0%93%d0%b5%d1%80%d0%be%d1%8f%d0%bc%20%d1%81%d0%bb%d0%b0%d0%b2%d0%b0%21";
      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestSimpleFieldListOfIdsIsProcessedCorrectly()
    {
      string[] fields = { "{0000-1111-2222}", "{1123-5813-21-34}" };
      var scope = new ScopeParameters();
      scope.AddScope(ScopeType.Children);
      string result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Min, scope, fields));
      Assert.AreEqual("payload=min&scope=c&fields=%7b0000-1111-2222%7d|%7b1123-5813-21-34%7d", result);
    }

    [Test]
    public void TestMixedFieldList()
    {
      string[] fields = { "Слава Україні!", "{0000-1111-2222}", "Героям слава!" };

      string result = this.builder.BuildUrlString(new QueryParameters(PayloadType.Content, null, fields));
      string expected = "payload=content&fields=%d0%a1%d0%bb%d0%b0%d0%b2%d0%b0%20%d0%a3%d0%ba%d1%80%d0%b0%d1%97%d0%bd%d1%96%21|%7b0000-1111-2222%7d|%d0%93%d0%b5%d1%80%d0%be%d1%8f%d0%bc%20%d1%81%d0%bb%d0%b0%d0%b2%d0%b0%21";
      Assert.AreEqual(expected, result);
    }

    [Test]
    public void TestFieldsOnly()
    {
      string[] fields = { "Слава Україні!", "{0000-1111-2222}", "Героям слава!" };

      string result = this.builder.BuildUrlString(new QueryParameters(null, null, fields));
      string expected = "fields=%d0%a1%d0%bb%d0%b0%d0%b2%d0%b0%20%d0%a3%d0%ba%d1%80%d0%b0%d1%97%d0%bd%d1%96%21|%7b0000-1111-2222%7d|%d0%93%d0%b5%d1%80%d0%be%d1%8f%d0%bc%20%d1%81%d0%bb%d0%b0%d0%b2%d0%b0%21";
      Assert.AreEqual(expected, result);
    }
  }
}