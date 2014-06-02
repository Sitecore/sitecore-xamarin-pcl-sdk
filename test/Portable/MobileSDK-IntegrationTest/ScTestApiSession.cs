using Sitecore.MobileSDK.PublicKey;


namespace MobileSDKIntegrationTest
{
    using System;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.Items;
    using System.Threading.Tasks;


    public class ScTestApiSession : ScApiSession
    {
        public ScTestApiSession(SessionConfig config, ItemSource defaultSource) : 
        base( config, defaultSource)
        {
            this.GetPublicKeyInvocationsCount = 0;
        }

        public async Task<PublicKeyX509Certificate> GetPublicKeyAsync_Public()
        {
            return await this.GetPublicKeyAsync ();
        }

        public async Task<ICredentialsHeadersCryptor> GetCredentialsCryptorAsync_Public()
        {
            return await this.GetCredentialsCryptorAsync ();
        }


        protected override async Task<PublicKeyX509Certificate> GetPublicKeyAsync()
        {
            ++this.GetPublicKeyInvocationsCount;
            return await base.GetPublicKeyAsync ();
        }

        protected override async Task<ICredentialsHeadersCryptor> GetCredentialsCryptorAsync()
        {
            return await base.GetCredentialsCryptorAsync ();
        }

        public int GetPublicKeyInvocationsCount { get; private set; }
    }
}

