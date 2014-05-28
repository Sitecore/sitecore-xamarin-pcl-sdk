using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK.TaskFlow;
using Sitecore.MobileSDK.PublicKey;

namespace Sitecore.MobileSDK
{
	public class GetItemsTasks : IRestApiCallTasks<string, string, ScItemsResponse>
	{
		#region Private Variables

		private readonly HttpClient httpClient;
		private readonly ItemRequestConfig requestConfig;

		#endregion Private Variables

		public GetItemsTasks (HttpClient httpClient, ItemRequestConfig config)
		{
			this.httpClient = httpClient;
			this.requestConfig = config;
		}

		#region IRestApiCallTasks

		public async Task<string> BuildRequestUrlForRequestAsync (string instanceUrl)
		{
			return await Task.Factory.StartNew (() =>
			{
				string finalUrl = instanceUrl + "/-/item/v1/";
				if (!string.IsNullOrEmpty (this.requestConfig.Id))
				{
					finalUrl += "?sc_itemid=" + this.requestConfig.Id;
				}

				return finalUrl;
			});
		}

		public async Task<string> SendRequestForUrlAsync (string requestUrl)
		{
			Func<HttpRequestMessage> setupEncryptedHeaders = () =>
			{
				HttpRequestMessage innerRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUrl);

				if (!this.requestConfig.SessionConfig.IsAnonymous ())
				{
					EncryptionUtil cryptor = new EncryptionUtil(this.requestConfig.Certificate);

					var encryptedLogin = cryptor.Encrypt (this.requestConfig.SessionConfig.Login);
					var encryptedPassword = cryptor.Encrypt (this.requestConfig.SessionConfig.Password);

					innerRequestMessage.Headers.Add("X-Scitemwebapi-Username", encryptedLogin);
					innerRequestMessage.Headers.Add("X-Scitemwebapi-Password", encryptedPassword);
					innerRequestMessage.Headers.Add("X-Scitemwebapi-Encrypted", "1");
				}

				return innerRequestMessage;
			};
			HttpRequestMessage requestMessage = await Task.Factory.StartNew ( setupEncryptedHeaders );

			HttpResponseMessage response = await this.httpClient.SendAsync (requestMessage);
			return await response.Content.ReadAsStringAsync();
		}

		// disposes httpData
		public async Task<ScItemsResponse> ParseResponseDataAsync (string data)
		{
			Func<ScItemsResponse> syncParseResponse = () =>
			{
				return ScItemsParser.Parse (data);
			};
			return await Task.Factory.StartNew (syncParseResponse);
		}

		#endregion IRestApiCallTasks
	}
}