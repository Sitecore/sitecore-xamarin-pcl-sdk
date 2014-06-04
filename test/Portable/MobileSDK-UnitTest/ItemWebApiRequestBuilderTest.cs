
namespace Sitecore.MobileSdkUnitTest
{
    using System;
    using NUnit.Framework;


    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;
    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;



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
        }
    }
}

