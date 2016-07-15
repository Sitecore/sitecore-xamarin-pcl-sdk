namespace Sitecore.MobileSdkUnitTest
{
  using System;
  using NUnit.Framework;

  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.MockObjects;
  using Sitecore.MobileSDK.UrlBuilder;
  using Sitecore.MobileSDK.UrlBuilder.Rest;
  using Sitecore.MobileSDK.UrlBuilder.SSC;

  [TestFixture]
  public class ItemSourceUrlBuilderTest
  {
    [Test]
    public void TestUrlBuilderRejectsNilSource()
    {
      TestDelegate createBuilderAction = () => new ItemSourceUrlBuilder(RestServiceGrammar.ItemSSCV2Grammar(), SSCUrlParameters.ItemSSCV2UrlParameters(), null);
      Assert.Throws<ArgumentNullException>(createBuilderAction);
    }

    [Test]
    public void TestUrlBuilderRejectsNilGrammar()
    {
      TestDelegate createBuilderAction = () => new ItemSourceUrlBuilder(null, SSCUrlParameters.ItemSSCV2UrlParameters(), LegacyConstants.DefaultSource());
      Assert.Throws<ArgumentNullException>(createBuilderAction);
    }

    [Test]
    public void TestSerializeDafaultSource()
    {
      ItemSource data = LegacyConstants.DefaultSource();
      ItemSourceUrlBuilder builder = new ItemSourceUrlBuilder(RestServiceGrammar.ItemSSCV2Grammar(), SSCUrlParameters.ItemSSCV2UrlParameters(), data);

      string expected = "sc_database=web&language=en";
      Assert.AreEqual(expected, builder.BuildUrlQueryString());
    }

    [Test]
    public void TestSerializationSupportsVersion()
    {
      ItemSource data = new ItemSource("master", "da", 100500);
      ItemSourceUrlBuilder builder = new ItemSourceUrlBuilder(RestServiceGrammar.ItemSSCV2Grammar(), SSCUrlParameters.ItemSSCV2UrlParameters(), data);

      string expected = "sc_database=master&language=da&sc_itemversion=100500";
      Assert.AreEqual(expected, builder.BuildUrlQueryString());
    }

    [Test]
    public void TestUrlBuilderExcapesArgs()
    {
      ItemSource data = new ItemSource("{master}", "da???", 123);
      ItemSourceUrlBuilder builder = new ItemSourceUrlBuilder(RestServiceGrammar.ItemSSCV2Grammar(), SSCUrlParameters.ItemSSCV2UrlParameters(), data);

      string expected = "sc_database=%7bmaster%7d&language=da%3f%3f%3f&sc_itemversion=123";
      Assert.AreEqual(expected, builder.BuildUrlQueryString());
    }
  }
}

