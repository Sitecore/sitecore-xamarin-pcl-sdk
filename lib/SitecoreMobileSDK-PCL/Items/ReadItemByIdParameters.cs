namespace Sitecore.MobileSDK
{
    using Sitecore.MobileSDK.PublicKey;
    using Sitecore.MobileSDK.UrlBuilder;

    public class ReadItemByIdParameters : IRequestConfig
    {
        public ReadItemByIdParameters(string instanceUrl, string webApiVersion, string itemId, ICredentialsHeadersCryptor cryptor)
        {
            this.InstanceUrl = instanceUrl;
            this.WebApiVersion = webApiVersion;
            this.ItemId = itemId;
            this.CredentialsHeadersCryptor = cryptor;
        }

        public string InstanceUrl { get; set; }

        public string WebApiVersion { get; private set; }

        public string ItemId { get; private set; }

        public ICredentialsHeadersCryptor CredentialsHeadersCryptor { get; private set; }
    }
}

