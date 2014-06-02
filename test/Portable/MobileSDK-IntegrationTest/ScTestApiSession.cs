using System;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;
using System.Threading.Tasks;

namespace MobileSDKIntegrationTest
{
    public class ScTestApiSession : ScApiSession
    {
        public ScTestApiSession(SessionConfig config, ItemSource defaultSource) : 
        base( config, defaultSource)
        {
        }

        public async Task<PublicKeyX509Certificate> GetPublicKeyAsync()
        {
            return await base.GetPublicKeyAsync ();
        }
    }
}

