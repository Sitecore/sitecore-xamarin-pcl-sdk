
namespace Sitecore.MobileSDK.CrudTasks
{
	using System;
	using Sitecore.MobileSDK.PublicKey;


	public class ItemRequestConfig
	{
		public string Id { get; private set; }
		public string InstanceUrl { get; private set; }
		public ICredentialsHeadersCryptor CredentialsCryptor { get; private set; }
		


		public ItemRequestConfig (string instanceUrl, string itemId, ICredentialsHeadersCryptor cryptor)
		{
			this.Id = itemId;
			this.InstanceUrl = instanceUrl;
			this.CredentialsCryptor = cryptor;

			this.Validate ();
		}

		private void Validate()
		{
			// TODO : implement me
		}
	}
}

