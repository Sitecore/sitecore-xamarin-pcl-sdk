

namespace Sitecore.MobileSDK.Items
{
    using Sitecore.MobileSDK.PublicKey;
    using Sitecore.MobileSDK.SessionSettings;

    using Sitecore.MobileSDK.UrlBuilder.ItemByPath;

    public class ReadItemByPathParameters : IGetItemByPathRequest, ICredentialCryptorOwner
    {
        public ReadItemByPathParameters(
            ISessionConfig sessionSettings, 
            IItemSource itemSource, 
            string itemPath, 
            ICredentialsHeadersCryptor cryptor = null)
        {
            this.SessionSettings = sessionSettings;
            this.ItemSource = itemSource;
            this.ItemPath = itemPath;
            this.CredentialsHeadersCryptor = cryptor;
        }

        public string ItemPath { get; private set; }

        public ICredentialsHeadersCryptor CredentialsHeadersCryptor { get; private set; }

        public IItemSource ItemSource { get; private set; }

        public ISessionConfig SessionSettings { get; private set; }

        public ICredentialsHeadersCryptor CredentialsCryptor { get; private set; }
    }
}