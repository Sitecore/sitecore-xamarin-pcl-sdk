namespace Sitecore.MobileSDK
{
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.PublicKey;
    using Sitecore.MobileSDK.SessionSettings;
    using Sitecore.MobileSDK.UrlBuilder;

    public class GetItemsByIdParameters : IGetItemByIdRequest
    {
        public GetItemsByIdParameters(ISessionConfig sessionSettings, IItemSource itemSource, string itemId, ICredentialsHeadersCryptor cryptor = null)
        {
            this.SessionSettings = sessionSettings;
            this.ItemSource = itemSource;
            this.ItemId = itemId;
            this.CredentialsHeadersCryptor = cryptor;
        }

        public string ItemId { get; private set; }

        public ICredentialsHeadersCryptor CredentialsHeadersCryptor { get; private set; }

        public IItemSource ItemSource { get; private set; }

        public ISessionConfig SessionSettings { get; private set; }

        public ICredentialsHeadersCryptor CredentialsCryptor { get; private set; }
    }
}

