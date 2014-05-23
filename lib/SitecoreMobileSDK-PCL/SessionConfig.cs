namespace Sitecore.MobileSDK
{
    using System;

    public class SessionConfig
    {
        public SessionConfig(string instanceUrl, string login, string password)
        {
            this.InstanceUrl = instanceUrl;
            this.Login = login;
            this.Password = password;
        }

        public string InstanceUrl
        {
            get;
            private set;
        }

        public string Login
        {
            get;
            private set;
        }

        public string Password
        {
            get;
            private set;
        }

        public bool IsAnonymous()
        {
            return string.IsNullOrEmpty(this.Login) && string.IsNullOrEmpty(this.Password);
        }
    }
}

