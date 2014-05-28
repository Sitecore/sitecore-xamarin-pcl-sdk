namespace MobileSDK_UnitTest_Desktop
{
    using System;
    using MobileSDKUnitTest.Mock;
    using NUnit.Framework;

    using Sitecore.MobileSDK.UrlBuilder;
    using Sitecore.MobileSDK.CrudTasks;

    [TestFixture]
    public class UrlBuildingTests
    {

        [Test]
        public void TestBuildValidParams()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters();
            mockParams.InstanceUrl = "https://localhost:80";
            mockParams.WebApiVersion = "v1";
            mockParams.ItemId = "{000-111-222-333}";


            IRequestConfig itemInfo = mockParams;
            WebApiUrlBuilder builder = new WebApiUrlBuilder();

            string result = builder.GetUrlForRequest(itemInfo);
            string expected = "https://localhost:80/-/item/v1?sc_itemid=%7B000-111-222-333%7d".ToLower();

            Assert.AreEqual( expected, result );
        }


        [Test]
        public void TestUrlSchemeIsHttpByDefault()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "SITECORE.net",
                WebApiVersion = "V100500",
                ItemId = "{TEST-GUID-666-13-666}"
            };

            IRequestConfig itemInfo = mockParams;
            WebApiUrlBuilder builder = new WebApiUrlBuilder();

            string result = builder.GetUrlForRequest(itemInfo);
            string expected = "http://sitecore.net/-/item/v100500?sc_itemid=%7btest-guid-666-13-666%7d";

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void TestBuildInvalidParams()
        {
            WebApiUrlBuilder builder = new WebApiUrlBuilder();
            TestDelegate action = () =>
            {
                string result = builder.GetUrlForRequest(null);
            };

            Assert.Throws<ArgumentNullException>(action);
        }

        [Test]
        public void TestInvalidItemId()
        {
            MockReadItemByIdParameters mockParams = new MockReadItemByIdParameters
            {
                InstanceUrl = "SITECORE.net",
                WebApiVersion = "V100500",
                ItemId = "/path/to/item"
            };

            IRequestConfig itemInfo = mockParams;
            WebApiUrlBuilder builder = new WebApiUrlBuilder();

            TestDelegate action = () =>
            {
                string result = builder.GetUrlForRequest(itemInfo);
            };

            Assert.Throws<ArgumentException>(action);
        }
    }
}
