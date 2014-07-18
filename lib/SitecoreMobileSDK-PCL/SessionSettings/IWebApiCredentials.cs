
namespace Sitecore.MobileSDK.SessionSettings
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;


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
