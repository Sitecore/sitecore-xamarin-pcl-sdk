
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
    }
}

