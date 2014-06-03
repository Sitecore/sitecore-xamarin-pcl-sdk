using System.Configuration;

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
    using Sitecore.MobileSDK.SessionSettings;


    [TestFixture]
    public class GetPublicKeyTest
    {

        private HttpClient _httpClient;
        private string _authenticatedUrl;
        private string _adminUsername;
        private string _adminPassword;
        private string _itemId;

        [SetUp]
        public void Setup()
        {
            this._httpClient = new HttpClient();
            this._authenticatedUrl = ConfigurationManager.AppSettings["authenticatedInstanceURL"];
            this._adminUsername = ConfigurationManager.AppSettings["adminUsername"];
            this._adminPassword = ConfigurationManager.AppSettings["adminPassword"];
            this._itemId = ConfigurationManager.AppSettings["HomeItemId"];
        }

        [TearDown]
        public void TearDown()
        {
            this._httpClient = null;
        }

        [Test]
        public async void TestGetItemAsAuthenticatedUser()
        {
            var config = new SessionConfig(_authenticatedUrl, _adminUsername, _adminPassword);
            var session = new ScApiSession(config, ItemSource.DefaultSource());

            var response = await session.ReadItemByIdAsync(_itemId);
            Assert.AreEqual(1, response.Items.Count);
            Assert.AreEqual("Home", response.Items[0].DisplayName);
        }

        [Test]
        public async void TestGetPublicKeyWithNotExistentInstanceUrl()
        {
            SessionConfig config = new SessionConfig("http://mobiledev1ua1.dddk.sitecore.net", _adminUsername, _adminPassword);
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

            try
            {
                await session.ReadItemByIdAsync(_itemId);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Sitecore.MobileSDK.ScAuthenticationException", exception.GetType().ToString());
                Assert.True(exception.Message.Contains("Unable to connect to the specified url"));
            }
        }

        [Test]
        public async void TestGetItemWithNullInstanceUrl()
        {
            var exception = Assert.Throws<ArgumentNullException> (() => new SessionConfig (null, _adminUsername, _adminPassword));
            Assert.IsTrue( 
                exception.GetBaseException().ToString().Contains("SessionConfig.InstanceUrl is required") 
            );
        }

        [Test]
        public void TestGetItemWithNullItemsSource()
        {
            SessionConfig config = new SessionConfig(_authenticatedUrl, _adminUsername, _adminPassword);

            TestDelegate action = () => new ScApiSession(config, null);
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(action, "we should get exception here");

            Assert.IsTrue( 
                exception.GetBaseException().ToString().Contains("ScApiSession.defaultSource cannot be null") 
            );
        }

        [Test]
        public async void TestGetItemWithEmptyPassword()
        {
            SessionConfig config = new SessionConfig(_authenticatedUrl, _adminUsername, "");
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

            try
            {
                await session.ReadItemByIdAsync(_itemId);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Sitecore.MobileSDK.ScAuthenticationException", exception.GetType().ToString());
                Assert.True(exception.Message.Contains("Unable to login with specified username and password"));
            }
        }

        [Test]
        public async void TestGetItemWithNotExistentUser()
        {
            SessionConfig config = new SessionConfig(_authenticatedUrl, "sitecore\\notexistent", "notexistent");
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

            try
            {
                await session.ReadItemByIdAsync(_itemId);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Sitecore.MobileSDK.ScAuthenticationException", exception.GetType().ToString());
                string s = exception.Message;
                Assert.True(exception.Message.Contains("Unable to login with specified username and password"));
            }
        }

        [Test]
        public async void TestGetItemWithInvalidUsernameAndPassword()
        {
            SessionConfig config = new SessionConfig(_authenticatedUrl, "inval|d u$er№ame", null);
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

            try
            {
                await session.ReadItemByIdAsync(_itemId);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Sitecore.MobileSDK.ScAuthenticationException", exception.GetType().ToString());
                Assert.True(exception.Message.Contains("Unable to login with specified username and password"));
            }
        }

        [Test]
        public async void TestGetItemAsAnonymousWithoutReadAccess()
        {
            SessionConfig config = new SessionConfig(_authenticatedUrl, null, null);
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());
            
            try
            {
                await session.ReadItemByIdAsync(_itemId);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Sitecore.MobileSDK.ScAuthenticationException", exception.GetType().ToString());
                Assert.True(exception.Message.Contains("Access to site is not granted"));
            }
        }
    }
}
