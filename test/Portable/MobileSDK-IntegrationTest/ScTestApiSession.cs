using Sitecore.MobileSDK.PublicKey;
using System.Threading;


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

        public async Task<PublicKeyX509Certificate> GetPublicKeyAsync_Public(CancellationToken cancelToken = default(CancellationToken))
        {
            return await this.GetPublicKeyAsync (cancelToken);
        }

        public async Task<ICredentialsHeadersCryptor> GetCredentialsCryptorAsync_Public(CancellationToken cancelToken = default(CancellationToken))
        {
            return await this.GetCredentialsCryptorAsync (cancelToken);
        }


        protected override async Task<PublicKeyX509Certificate> GetPublicKeyAsync(CancellationToken cancelToken = default(CancellationToken) )
        {
            ++this.GetPublicKeyInvocationsCount;
            return await base.GetPublicKeyAsync (cancelToken);
        }

        protected override async Task<ICredentialsHeadersCryptor> GetCredentialsCryptorAsync(CancellationToken cancelToken = default(CancellationToken))
        {
            return await base.GetCredentialsCryptorAsync (cancelToken);
        }

        public int GetPublicKeyInvocationsCount { get; private set; }
    }
}

