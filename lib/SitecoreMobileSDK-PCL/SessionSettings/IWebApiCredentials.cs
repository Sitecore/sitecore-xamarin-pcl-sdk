using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sitecore.MobileSDK.SessionSettings
{
    interface IWebApiCredentials
    {
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
