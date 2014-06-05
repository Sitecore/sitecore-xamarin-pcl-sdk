
namespace Sitecore.MobileSdkUnitTest
{
    using System;
    using NUnit.Framework;


    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
    using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;


    [TestFixture]
    public class ItemWebApiRequestBuilderTest
    {
        #region ItemId
        [Test]
        public void TestItemIdRequestBuilderWithAllFields()
        {
            ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();

            IReadItemsByIdRequest result =  builder.RequestWithId ("{dead-beef}")
                .Database ("web")
                .Language ("en")
                .Version ("1")
                .Build();

            Assert.IsNotNull (result);
            Assert.IsNotNull (result.ItemSource);
            Assert.IsNotNull (result.ItemId);
            Assert.IsNull (result.SessionSettings);


            Assert.AreEqual ("{dead-beef}", result.ItemId);
            Assert.AreEqual ("en", result.ItemSource.Language);
            Assert.AreEqual ("web", result.ItemSource.Database);
            Assert.AreEqual ("1", result.ItemSource.Version);
        }

        [Test]
        public void TestItemIdRequestBuilderWithIdOnly()
        {
            ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();

            IReadItemsByIdRequest result =  builder.RequestWithId ("{abra-kadabra}").Build();

            Assert.IsNotNull (result);
            Assert.IsNotNull (result.ItemSource);
            Assert.IsNotNull (result.ItemId);
            Assert.IsNull (result.SessionSettings);


            Assert.AreEqual ("{abra-kadabra}", result.ItemId);
            Assert.IsNull (result.ItemSource.Language);
            Assert.IsNull (result.ItemSource.Database);
            Assert.IsNull (result.ItemSource.Version);
        }

        [Test]
        public void TestItemIdRequestBuilderWithNullIdCrashes()
        {
            ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();
            Assert.Throws<ArgumentNullException> (() => builder.RequestWithId (null));
        }

        [Test]
        public void TestItemIdRequestBuilderWithEmptyIdCrashes()
        {
            ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();
            Assert.Throws<ArgumentNullException> (() => builder.RequestWithId (""));
        }

        [Test]
        public void TestItemIdRequestBuilderWithWhitespaceIdCrashes()
        {
            ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();
            Assert.Throws<ArgumentNullException> (() => builder.RequestWithId ("\t \r \n"));
        }

        [Test]
        public void TestItemIdWithoutBracesCrashesBuilder()
        {
            ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();
            Assert.Throws<ArgumentException> (() => builder.RequestWithId ("ololololo"));
        }

        [Test]
        public void TestItemIdWithBracesOnlyCrashesBuilder()
        {
            ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();
            Assert.Throws<ArgumentException> (() => builder.RequestWithId ("{}"));
        }
        #endregion ItemId


        #region ItemPath
        [Test]
        public void TestItemPathRequestBuilderWithAllFields()
        {
            ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();

            IReadItemsByPathRequest result =  builder.RequestWithPath ("/sitecore/content")
                .Database ("master")
                .Language ("da")
                .Version ("100500")
                .Build();


            Assert.IsNotNull (result);
            Assert.IsNotNull (result.ItemSource);
            Assert.IsNotNull (result.ItemPath);
            Assert.IsNull (result.SessionSettings);

            Assert.AreEqual ("/sitecore/content", result.ItemPath);
            Assert.AreEqual ("da", result.ItemSource.Language);
            Assert.AreEqual ("master", result.ItemSource.Database);
            Assert.AreEqual ("100500", result.ItemSource.Version);
        }
        #endregion ItemPath


        #region SitecoreQuery
        [Test]
        public void TestSitecoreQueryRequestBuilderWithAllFields()
        {
            ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();

            IReadItemsByQueryRequest result =  builder.RequestWithSitecoreQuery ("fast:/sitecore/content/HOME/*")
                .Database ("core")
                .Language ("de")
                .Version ("341")
                .Build();


            Assert.IsNotNull (result);
            Assert.IsNotNull (result.ItemSource);
            Assert.IsNotNull (result.SitecoreQuery);
            Assert.IsNull (result.SessionSettings);

            Assert.AreEqual ("fast:/sitecore/content/HOME/*", result.SitecoreQuery);
            Assert.AreEqual ("de", result.ItemSource.Language);
            Assert.AreEqual ("core", result.ItemSource.Database);
            Assert.AreEqual ("341", result.ItemSource.Version);
        }
        #endregion SitecoreQuery
    }
}

