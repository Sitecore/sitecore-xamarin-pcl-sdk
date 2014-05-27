using Sitecore.MobileSDK.Items;

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
        #region Private Variables

        private readonly HttpClient httpClient;

        private readonly SessionConfig sessionConfig;

        private PublicKeyX509Certificate publicCertifiacte;

        #endregion Private Variables

        public ScApiSession(SessionConfig config)
        {
            this.sessionConfig = config;
            this.httpClient = new HttpClient();
        }

        #region Forbidden Methods

        private ScApiSession()
        {
        }

        #endregion Forbidden Methods

        #region Encryption

        public async Task<PublicKeyX509Certificate> GetPublicKey()
        {
            GetPublicKeyTasks taskFlow = new GetPublicKeyTasks(this.httpClient);

            PublicKeyX509Certificate result = await RestApiCallFlow.LoadRequestFromNetworkFlow(this.sessionConfig.InstanceUrl, taskFlow);
            this.publicCertifiacte = result;

            return result;
        }

        public string EncryptString(string data)
        {
            EncryptionUtil cryptor = new EncryptionUtil(this.publicCertifiacte);
            return cryptor.Encrypt(data);
        }

        #endregion Encryption

		#region GetItems
		public async Task<ScItemsResponse> GetItemById (string id)
		{
			var config = new ItemRequestConfig (this.sessionConfigm); 
			config.Id = id;

			var taskFlow = new GetItemsTasks(this.httpClient, config);

			return await RestApiCallFlow.LoadRequestFromNetworkFlow("", taskFlow);
		}
		#endregion GetItems
    }
}