using Sitecore.MobileSDK;

namespace Sitecore.MobileSdkUnitTest
{
    using System;
    using NUnit.Framework;

    using Sitecore.MobileSDK.UrlBuilder;



    [TestFixture]
    public class ItemSourceUrlBuilderTest
    {
        [Test]
        public void TestUrlBuilderRejectsNilSource()
        {
            TestDelegate createBuilderAction = () => new ItemSourceUrlBuilder (RestServiceGrammar.ItemWebApiV2Grammar (), WebApiUrlParameters.ItemWebApiV2UrlParameters(), null);
            Assert.Throws<ArgumentNullException> (createBuilderAction);
        }

        [Test]
        public void TestUrlBuilderRejectsNilGrammar()
        {
            TestDelegate createBuilderAction = () => new ItemSourceUrlBuilder (null, WebApiUrlParameters.ItemWebApiV2UrlParameters(), ItemSource.DefaultSource());
            Assert.Throws<ArgumentNullException> (createBuilderAction);
        }

        [Test]
        public void TestSerializeDafaultSource()
        {
            ItemSource data = ItemSource.DefaultSource ();
            ItemSourceUrlBuilder builder = new ItemSourceUrlBuilder (RestServiceGrammar.ItemWebApiV2Grammar (), WebApiUrlParameters.ItemWebApiV2UrlParameters(), data);

            string expected = "sc_database=web&sc_lang=en";
            Assert.AreEqual (builder.BuildUrlQueryString(), expected);
        }

        [Test]
        public void TestSerializationSupportsVersion()
        {
            ItemSource data = new ItemSource ("master", "da", "100500");
            ItemSourceUrlBuilder builder = new ItemSourceUrlBuilder (RestServiceGrammar.ItemWebApiV2Grammar (), WebApiUrlParameters.ItemWebApiV2UrlParameters(), data);

            string expected = "sc_database=master&sc_lang=da&sc_itemversion=100500";
            Assert.AreEqual (builder.BuildUrlQueryString(), expected);
        }
    }
}

