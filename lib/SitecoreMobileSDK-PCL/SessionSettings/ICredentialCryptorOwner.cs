namespace Sitecore.MobileSDK.SessionSettings
{
    using Sitecore.MobileSDK.PublicKey;

    public interface ICredentialCryptorOwner
    {
        ICredentialsHeadersCryptor CredentialsCryptor { get; }
    }
}
