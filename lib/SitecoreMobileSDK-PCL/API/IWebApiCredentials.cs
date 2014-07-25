
namespace Sitecore.MobileSDK.API
{
    public interface IWebApiCredentials
  {
    IWebApiCredentials CredentialsShallowCopy();

    string Username
    {
      get;
    }

    string Password
    {
      get;
    }
  }
}
