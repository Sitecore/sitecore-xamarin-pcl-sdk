
namespace Sitecore.MobileSDK.API
{
    public interface IWebApiCredentials
  {
    IWebApiCredentials CredentialsShallowCopy();

    string UserName
    {
      get;
    }

    string Password
    {
      get;
    }
  }
}
