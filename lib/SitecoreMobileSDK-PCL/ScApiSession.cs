

namespace Sitecore.MobileSDK
{
    using System;
    using System.Diagnostics;
    using System.IO;
	using System.Text;
    using System.Net.Http;
    using System.Threading.Tasks;
    
    
	using Org.BouncyCastle.Crypto;
    using Org.BouncyCastle.Crypto.Parameters;
    using Org.BouncyCastle.Math;
    using Org.BouncyCastle.Security;

    using Sitecore.MobileSDK.PublicKey;
	using Sitecore.MobileSDK.TaskFlow;

    public class ScApiSession
    {
        private readonly HttpClient httpClient;
        private readonly SessionConfig sessionConfig;
        private PublicKeyX509Certificate publicCertifiacte;

        public ScApiSession(SessionConfig config)
        {
            this.sessionConfig = config;
            this.httpClient = new HttpClient();
        }

        private ScApiSession()
        {
        }

        public async Task<PublicKeyX509Certificate> GetPublicKey()
        {
			GetPublicKeyTasks taskFlow = new GetPublicKeyTasks (this.httpClient);


			PublicKeyX509Certificate result = await RestApiCallFlow.LoadRequestFromNetworkFlow(this.sessionConfig.InstanceUrl, taskFlow);
			this.publicCertifiacte = result;

			return result;
		}

		public string EncryptString(string data)
        {
			EncryptionUtil cryptor = new EncryptionUtil (this.publicCertifiacte);
			return cryptor.Encrypt (data);
        }
    }
}