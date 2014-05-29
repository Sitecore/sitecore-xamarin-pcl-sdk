namespace Sitecore.MobileSDK.Items
{
    using Sitecore.MobileSDK.PublicKey;
    using Sitecore.MobileSDK.UrlBuilder;

    public class ReadItemByPathParameters : IRequestConfig
    {
        public ReadItemByPathParameters(string instanceUrl, string webApiVersion, string itemPath, ICredentialsHeadersCryptor cryptor)
        {
            this.InstanceUrl = instanceUrl;
            this.WebApiVersion = webApiVersion;
            this.ItemPath = itemPath;
            this.CredentialsHeadersCryptor = cryptor;
        }

        public string InstanceUrl { get; set; }

        public string WebApiVersion { get; private set; }

        public string ItemPath { get; private set; }

        public ICredentialsHeadersCryptor CredentialsHeadersCryptor { get; private set; }
    }
}