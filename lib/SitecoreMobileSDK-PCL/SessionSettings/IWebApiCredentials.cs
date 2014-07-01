
namespace Sitecore.MobileSDK.SessionSettings
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;


  public interface IWebApiCredentials
  {
    IWebApiCredentials CredentialsShallowCopy();

    string Login
    {
      get;
    }

    string Password
    {
      get;
    }
  }
}
