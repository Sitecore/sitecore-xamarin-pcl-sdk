namespace Sitecore.MobileSDK.SessionSettings
{
    using System;

    public class SessionConfig : ISessionConfig
    {
        public SessionConfig(string instanceUrl, string login, string password, string site = null)
        {
            this.InstanceUrl = instanceUrl;
            this.Login       = login      ;
            this.Password    = password   ;
            this.Site        = site       ;
        }

        public string InstanceUrl
        {
            get;
            private set;
        }

        public string Site      
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

