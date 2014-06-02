
namespace Sitecore.MobileSDK
{
	using System;
	using System.Net.Http;
	using System.Threading.Tasks;

	using Sitecore.MobileSDK.SessionSettings;
	using Sitecore.MobileSDK.CrudTasks;
	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.PublicKey;
	using Sitecore.MobileSDK.TaskFlow;
	using Sitecore.MobileSDK.UrlBuilder.Rest;
	using Sitecore.MobileSDK.UrlBuilder.WebApi;
	using Sitecore.MobileSDK.UrlBuilder.ItemById;
	using Sitecore.MobileSDK.UrlBuilder.ItemByPath;
	using Sitecore.MobileSDK.UrlBuilder.ItemByQuery;


    public class ScApiSession
    {
        public ScApiSession(SessionConfig config, ItemSource defaultSource)
        {
            if (null == config)
            {
                throw new ArgumentNullException("ScApiSession.config cannot be null");
            }
            if (null == defaultSource)
            {
                throw new ArgumentNullException("ScApiSession.defaultSource cannot be null");
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

        protected async Task<PublicKeyX509Certificate> GetPublicKeyAsync()
        {
            var taskFlow = new GetPublicKeyTasks(this.httpClient);

            PublicKeyX509Certificate result = await RestApiCallFlow.LoadRequestFromNetworkFlow(this.sessionConfig.InstanceUrl, taskFlow);
            this.publicCertifiacte = result;

            return result;
        }

        private async Task<ICredentialsHeadersCryptor> GetCredentialsCryptorAsync()
        {
            if (this.sessionConfig.IsAnonymous())
            {
                return new AnonymousSessionCryptor();
            }
            else
            {
                // TODO : flow should be responsible for caching. Do not hard code here
                this.publicCertifiacte = await this.GetPublicKeyAsync();
                return new AuthenticedSessionCryptor(this.sessionConfig.Login, this.sessionConfig.Password, this.publicCertifiacte);
            }
        }

        #endregion Encryption

        #region GetItems

		public async Task<ScItemsResponse> ReadItemByIdAsync(string id)
		{
			ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync();

            var config = new GetItemsByIdParameters(this.sessionConfig, ItemSource.DefaultSource(), id, cryptor);

			var taskFlow = new GetItemsByIdTasks(new ItemByIdUrlBuilder(this.restGrammar, this.webApiGrammar), this.httpClient);

			return await RestApiCallFlow.LoadRequestFromNetworkFlow(config, taskFlow);
		}

        public async Task<ScItemsResponse> ReadItemByPathAsync(string path)
        {
            ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync();

            var config = new ReadItemByPathParameters(
                this.sessionConfig, 
                this.defaultSource, 
                path, 
                cryptor);

            var taskFlow = new GetItemsByPathTasks(new ItemByPathUrlBuilder(this.restGrammar, this.webApiGrammar), this.httpClient);

            return await RestApiCallFlow.LoadRequestFromNetworkFlow(config, taskFlow);
        }

        public async Task<ScItemsResponse> ReadItemByQueryAsync(string sitecoreQuery)
        {
            ICredentialsHeadersCryptor cryptor = await this.GetCredentialsCryptorAsync();

            var config = new ReadItemByQueryParameters(
                this.sessionConfig, 
                this.defaultSource, 
                sitecoreQuery, 
                cryptor);

            var taskFlow = new GetItemsByQueryTasks(new ItemByQueryUrlBuilder(this.restGrammar, this.webApiGrammar), this.httpClient);

            return await RestApiCallFlow.LoadRequestFromNetworkFlow(config, taskFlow);
        }

        #endregion GetItems


        #region Private Variables

        private readonly HttpClient httpClient;

        private readonly SessionConfig sessionConfig;
        private readonly ItemSource defaultSource;

        private readonly IRestServiceGrammar restGrammar = RestServiceGrammar.ItemWebApiV2Grammar();
        private readonly IWebApiUrlParameters webApiGrammar = WebApiUrlParameters.ItemWebApiV2UrlParameters();


        private PublicKeyX509Certificate publicCertifiacte;

        #endregion Private Variables
    }
}