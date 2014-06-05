


namespace MobileSDKIntegrationTest
{
    using NUnit.Framework;
    using NUnit.Framework.Constraints;

    using System;
    using System.Security;
    using System.Net.Http;
    using System.Xml;


    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.UrlBuilder.ItemById;
    using Sitecore.MobileSDK.SessionSettings;


    [TestFixture]
    public class GetPublicKeyTest
    {
        private string authenticatedUrl;
        private string adminUsername;
        private string adminPassword;
        private string itemId;

        IReadItemsByIdRequest requestWithItemId;

        [SetUp]
        public void Setup()
        {
            var env = TestEnvironment.DefaultTestEnvironment();
            this.authenticatedUrl = env.AuthenticatedInstanceURL;
            this.adminUsername = env.AdminUsername;
            this.adminPassword = env.AdminPassword;
            this.itemId = env.HomeItemId;


            ItemWebApiRequestBuilder requestBuilder = new ItemWebApiRequestBuilder ();
            this.requestWithItemId = requestBuilder.RequestWithId (this.itemId).Build();
        }

        [TearDown]
        public void TearDown()
        {
            this.requestWithItemId = null;

            this.authenticatedUrl = null;
            this.adminUsername = null;
            this.adminPassword = null;
            this.itemId = null;
        }

        [Test]
        public async void TestGetItemAsAuthenticatedUser()
        {
            var config = new SessionConfig(authenticatedUrl, adminUsername, adminPassword);
            var session = new ScApiSession(config, ItemSource.DefaultSource());


            ItemWebApiRequestBuilder requestBuilder = new ItemWebApiRequestBuilder ();
            var request = requestBuilder.RequestWithId (this.itemId).Build();

            var response = await session.ReadItemByIdAsync(request);
            Assert.AreEqual(1, response.Items.Count);
            Assert.AreEqual("Home", response.Items[0].DisplayName);
        }

        [Test]
        public async void TestGetPublicKeyWithNotExistentInstanceUrl()
        {
            SessionConfig config = new SessionConfig("http://mobiledev1ua1.dddk.sitecore.net", adminUsername, adminPassword);
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

            try
            {
                await session.ReadItemByIdAsync(this.requestWithItemId);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Sitecore.MobileSDK.ScAuthenticationException", exception.GetType().ToString());
                Assert.True(exception.Message.Contains("Unable to connect to the specified url"));

                return;
            }

            Assert.Fail ("Excption not thrown");
        }

        [Test]
        public void TestGetItemWithNullInstanceUrl()
        {
            var exception = Assert.Throws<ArgumentNullException> (() => new SessionConfig (null, adminUsername, adminPassword));
            Assert.IsTrue( 
                exception.GetBaseException().ToString().Contains("SessionConfig.InstanceUrl is required") 
            );
        }

        [Test]
        public void TestGetItemWithNullItemsSource()
        {
            SessionConfig config = new SessionConfig(authenticatedUrl, adminUsername, adminPassword);

            TestDelegate action = () => new ScApiSession(config, null);
            ArgumentNullException exception = Assert.Throws<ArgumentNullException>(action, "we should get exception here");

            Assert.IsTrue( 
                exception.GetBaseException().ToString().Contains("ScApiSession.defaultSource cannot be null") 
            );
        }

        [Test]
        public async void TestGetItemWithEmptyPassword()
        {
            SessionConfig config = new SessionConfig(authenticatedUrl, adminUsername, "");
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

            try
            {
                await session.ReadItemByIdAsync(this.requestWithItemId);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Sitecore.MobileSDK.ScAuthenticationException", exception.GetType().ToString());
                Assert.True(exception.Message.Contains("Unable to login with specified username and password"));

                return;
            }

            Assert.Fail ("Exception not thrown");
        }

        [Test]
        public async void TestGetItemWithNotExistentUser()
        {
            SessionConfig config = new SessionConfig(authenticatedUrl, "sitecore\\notexistent", "notexistent");
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

            try
            {
                await session.ReadItemByIdAsync(this.requestWithItemId);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Sitecore.MobileSDK.ScAuthenticationException", exception.GetType().ToString());

                string message = exception.Message;
                Assert.True(message.Contains("Unable to login with specified username and password"));

                return;
            }

            Assert.Fail ("Exception not thrown");
        }

        [Test]
        public async void TestGetItemWithInvalidUsernameAndPassword()
        {
            SessionConfig config = new SessionConfig(authenticatedUrl, "inval|d u$er№ame", null);
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());

            try
            {
                await session.ReadItemByIdAsync(this.requestWithItemId);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Sitecore.MobileSDK.ScAuthenticationException", exception.GetType().ToString());
                Assert.True(exception.Message.Contains("Unable to login with specified username and password"));

                return;
            }

            Assert.Fail ("Exception not thrown");
        }

        [Test]
        public async void TestGetItemAsAnonymousWithoutReadAccess()
        {
            SessionConfig config = new SessionConfig(authenticatedUrl, null, null);
            ScApiSession session = new ScApiSession(config, ItemSource.DefaultSource());
            
            try
            {
                await session.ReadItemByIdAsync(this.requestWithItemId);
            }
            catch (Exception exception)
            {
                Assert.AreEqual("Sitecore.MobileSDK.ScAuthenticationException", exception.GetType().ToString());
                Assert.True(exception.Message.Contains("Access to site is not granted"));
                return;
            }

            Assert.Fail ("Exception not thrown");
        }
    }
}
