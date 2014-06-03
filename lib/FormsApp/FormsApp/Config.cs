namespace FormsApp
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using FormsApp.Annotations;

    public class Config : INotifyPropertyChanged
    {
        private string instanceUrl;
        private string login;
        private string password;
        private string site;
        private string database;

        public Config()
        {
            this.instanceUrl = "http://mobiledev1ua1.dk.sitecore.net:722/";
            this.login = "extranet\\creatorex";
            this.password = "creatorex";
            this.site = string.Empty;
            this.database = "web";
        }

        public string InstanceUrl
        {
            get
            {
                return this.instanceUrl;
            }

            set
            {
                if (value.Equals(this.instanceUrl, StringComparison.Ordinal))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }

                this.instanceUrl = value;
                this.OnPropertyChanged();
            }
        }

        public string Login
        {
            get
            {
                return this.login;
            }

            set
            {
                if (value.Equals(this.login, StringComparison.Ordinal))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }

                this.login = value;
                this.OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (value.Equals(this.password, StringComparison.Ordinal))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }

                this.password = value;
                this.OnPropertyChanged();
            }
        }

        public string Database
        {
            get
            {
                return this.database;
            }

            set
            {
                if (value.Equals(this.database, StringComparison.Ordinal))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }

                this.database = value;
                this.OnPropertyChanged();
            }
        }

        public string Site
        {
            get
            {
                return this.site;
            }

            set
            {
                if (value.Equals(this.site, StringComparison.Ordinal))
                {
                    // Nothing to do - the value hasn't changed;
                    return;
                }

                this.site = value;
                this.OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}