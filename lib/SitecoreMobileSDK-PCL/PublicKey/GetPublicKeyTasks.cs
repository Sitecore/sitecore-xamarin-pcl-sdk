

namespace Sitecore.MobileSDK.PublicKey
{
	using System;
	using System.IO;
	using System.Threading.Tasks;
	using Sitecore.MobileSDK.TaskFlow;


	public class GetPublicKeyTasks : IRestApiCallTasks<string, Stream, PublicKeyX509Certificate>
	{
		public async Task<string> BuildRequestUrlForRequestAsync( string instanceUrl )
		{
			return await Task.Factory.StartNew ( () => instanceUrl + "/-/item/v1/-/actions/getpublickey" );
		}

		public Task<Stream> SendRequestForUrlAsync( string requestUrl )
		{
			return null;
		}

		public Task<PublicKeyX509Certificate> ParseResponseDataAsync(Stream httpData)
		{
			return null;
		}
	}
}

