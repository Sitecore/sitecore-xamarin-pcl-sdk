using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK.PublicKey;

namespace MobileSDKIntegrationTest
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class CryptorConstructionTest
    {
        private ScTestApiSession anonymousSession;
        private ScTestApiSession authenticatedSession;

        [SetUp]
        public void SetUp()
        {
            SessionConfig config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", null, null);
            this.anonymousSession = new ScTestApiSession(config, ItemSource.DefaultSource());

            config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "sitecore\\admin", "b");
            this.authenticatedSession = new ScTestApiSession(config, ItemSource.DefaultSource());
        }

        [TearDown]
        public void TearDown()
        {
            this.anonymousSession     = null;
            this.authenticatedSession = null;
        }

        [Test]
        public async void TestAnonymousSessionDoesNotFetchPublicKey()
        {
            ICredentialsHeadersCryptor cryptor = await this.anonymousSession.GetCredentialsCryptorAsync_Public ();
            Assert.NotNull (cryptor);

            Assert.AreEqual (0, this.anonymousSession.GetPublicKeyInvocationsCount);
        }


        [Test]
        public async void TestAuthenticatedSessionDownloadsPublicKey()
        {
            ICredentialsHeadersCryptor cryptor = await this.authenticatedSession.GetCredentialsCryptorAsync_Public ();
            Assert.NotNull (cryptor);

            Assert.AreEqual (1, this.authenticatedSession.GetPublicKeyInvocationsCount);
        }
    }
}

