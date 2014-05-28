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
	using Sitecore.MobileSDK.CrudTasks;

    public class ScApiSession
    {
        #region Private Variables

        private readonly HttpClient httpClient;

        private readonly SessionConfig sessionConfig;
        private readonly ItemSource defaultSource;

        private PublicKeyX509Certificate publicCertifiacte;

        #endregion Private Variables

        public ScApiSession(SessionConfig config, ItemSource defaultSource)
        {
            if (null == config)
            {
                throw new ArgumentNullException ("ScApiSession.config cannot be null");
            }
            if (null == defaultSource)
            {
                throw new ArgumentNullException ("ScApiSession.defaultSource cannot be null");
            }


            this.sessionConfig = config;
            this.defaultSource = defaultSource;
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

		public async Task<ICredentialsHeadersCryptor> GetCredentialsCryptorAsync()
		{
			if (this.sessionConfig.IsAnonymous ())
			{
				return new AnonymousSessionCryptor ();
			}
			else
			{
				// TODO : flow should be responsible for caching. Do not hard code here
				PublicKeyX509Certificate cert = await this.GetPublicKey ();
				return new AuthenticedSessionCryptor (this.sessionConfig.Login, this.sessionConfig.Password, cert);
			}
		}

        #endregion Encryption

		#region GetItems
		public async Task<ScItemsResponse> GetItemById (string id)
		{
            PublicKeyX509Certificate cert = await GetPublicKey ();
			ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync ();
			ItemRequestConfig config = new ItemRequestConfig (this.sessionConfig.InstanceUrl, id, cryptor);

			var taskFlow = new GetItemsTasks(this.httpClient);

			return await RestApiCallFlow.LoadRequestFromNetworkFlow(config, taskFlow);
		}
		#endregion GetItems
    }
}