

namespace Sitecore.MobileSDK
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net.Http;
    using System.Threading.Tasks;
    
    using Sitecore.MobileSDK.PublicKey;
	using Sitecore.MobileSDK.TaskFlow;

    public class ScApiSession
    {
		#region Forbidden Methods

		private ScApiSession()
		{
		}

		#endregion Forbidden Methods

        public ScApiSession(SessionConfig config)
        {
            this.sessionConfig = config;
            this.httpClient = new HttpClient();
        }
			
		#region Encryption
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
		#endregion Encryption

		#region Private Variables

		private readonly HttpClient httpClient;
		private readonly SessionConfig sessionConfig;
		private PublicKeyX509Certificate publicCertifiacte;

		#endregion Private Variables
    }
}