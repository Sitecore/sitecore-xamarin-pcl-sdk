

namespace MobileSDKIntegrationTest
{
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    using System;
    using System.Security;
    using System.Net.Http;
    using System.Xml;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;

    [TestFixture]
    public class GetPublicKeyTest
    {
        private ScApiSession sessionWithAnonymousAccess;
        private ScApiSession sessionWithNoAnonymousAccess;

        private HttpClient httpClient;

        [SetUp]
        public void Setup()
        {
            SessionConfig config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "sitecore\\admin", "b");
            this.sessionWithNoAnonymousAccess = new ScApiSession(config, ItemSource.DefaultSource());

            config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:722", "sitecore\\admin", "b");
            this.sessionWithAnonymousAccess = new ScApiSession(config, ItemSource.DefaultSource());

            this.httpClient = new HttpClient();
        }

        [TearDown]
        public void TearDown()
        {
            this.sessionWithNoAnonymousAccess = null;
            this.sessionWithAnonymousAccess = null;
        }

        [Test]
        public async void TestRestrictedInstanceReturnsErrorByDefault()
        {
            string url = "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v1";
            string response = await this.httpClient.GetStringAsync(url);

            string expectedResponse = "{\"statusCode\":401,\"error\":{\"message\":\"Access to site is not granted.\"}}";
            Assert.AreEqual(response, expectedResponse);
        }

        [Test]
        public async void TestRestrictedInstanceReturnsItemsWhenAuthenticated()
        {
            PublicKeyX509Certificate publicKey = await this.sessionWithNoAnonymousAccess.GetPublicKey();
            string encryptedLogin = this.sessionWithNoAnonymousAccess.EncryptString("sitecore\\admin");
            string encryptedPassword = this.sessionWithNoAnonymousAccess.EncryptString("b");

            this.httpClient.DefaultRequestHeaders.Add("X-Scitemwebapi-Username", encryptedLogin);
            this.httpClient.DefaultRequestHeaders.Add("X-Scitemwebapi-Password", encryptedPassword);
            this.httpClient.DefaultRequestHeaders.Add("X-Scitemwebapi-Encrypted", "1");

            string url = "http://mobiledev1ua1.dk.sitecore.net:7119/-/item/v1";
            string response = await this.httpClient.GetStringAsync(url);


            JObject json = JObject.Parse(response);
            int statusCode = (int)json.SelectToken("$.statusCode");

            Assert.AreEqual(200, statusCode);
        }

        [Test]
        public async void TestGetPublicKeyWithInvalidInstanceUrl()
        {
            SessionConfig config = new SessionConfig("http://mobiledev1ua1.dddk.sitecore.net", "sitecore\\admin", "b");
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

            TestDelegate action = async () =>
            {
                var response = await session.GetItemById("{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}");
            };

            XmlException exception = Assert.Throws<XmlException>(action, "we should get error here");
        }

        [Test]
        public async void TestGetPublicKeyWithNullInstanceUrl()
        {
            SessionConfig config = new SessionConfig(null, "sitecore\\admin", "b");
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

            TestDelegate action = async () =>
            {
                var response = await session.GetItemById("{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}");
            };

            InvalidOperationException exception = Assert.Throws<InvalidOperationException>(action, "we should get error here");
            StringAssert.Contains("An invalid request URI was provided.", exception.GetBaseException().ToString());
        }

        [Test]
        public async void TestGetPublicKeyWithNullItemsSource()
        {
            SessionConfig config = new SessionConfig(null, "sitecore\\admin", "b");
            

            TestDelegate action = async () =>
            {
                ScApiSession session = new ScApiSession(config, null);
            };

            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(action, "we should get error here");
            StringAssert.Contains("ScApiSession.defaultSource cannot be null", exception.GetBaseException().ToString());
        }
    }
}
