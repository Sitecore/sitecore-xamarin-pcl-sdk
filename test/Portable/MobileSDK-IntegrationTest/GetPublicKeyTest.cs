namespace MobileSDKIntegrationTest
{
    using NUnit.Framework;

    using System.Net.Http;

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
            SessionConfig config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "extranet\\creatorex", "creatorex");
            this.sessionWithNoAnonymousAccess = new ScApiSession(config, ItemSource.DefaultSource());

            config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:722", "extranet\\creatorex", "creatorex");
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


            string expectedResponse = "{\"statusCode\":200,\"result\":{\"totalCount\":1,\"resultCount\":1,\"items\":[{\"Category\":\"Content\",\"Database\":\"web\",\"DisplayName\":\"Home\",\"HasChildren\":true,\"Icon\":\"/temp/IconCache/Network/16x16/home.png\",\"ID\":\"{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\",\"Language\":\"en\",\"LongID\":\"/{11111111-1111-1111-1111-111111111111}/{0DE95AE4-41AB-4D01-9EB0-67441B7C2450}/{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}\",\"MediaUrl\":\"/~/icon/Network/48x48/home.png.aspx\",\"Name\":\"Home\",\"Path\":\"/sitecore/content/Home\",\"Template\":\"Sample/Sample Item\",\"TemplateId\":\"{76036F5E-CBCE-46D1-AF0A-4143F9B557AA}\",\"TemplateName\":\"Sample Item\",\"Url\":\"~/link.aspx?_id=110D559FDEA542EA9C1C8A5DF7E70EF9\\u0026amp;_z=z\",\"Version\":1,\"Fields\":{\"{75577384-3C97-45DA-A847-81B00500E250}\":{\"Name\":\"Title\",\"Type\":\"text\",\"Value\":\"Sitecore master\"},\"{A60ACD61-A6DB-4182-8329-C957982CEC74}\":{\"Name\":\"Text\",\"Type\":\"Rich Text\",\"Value\":\"\\u003cdiv\\u003eWelcome to Sitecore!\\u003c/div\\u003e\\n\\u003cdiv\\u003e\\u003cbr /\\u003e\\n\\u003c/div\\u003e\\n\\u003ca href=\\\"~/link.aspx?_id=A2EE64D5BD7A4567A27E708440CAA9CD\\u0026amp;_z=z\\\"\\u003eAccelerometer\\u003c/a\\u003e\"}}}]}}";
            Assert.AreEqual(response, expectedResponse);
        }
    }
}
