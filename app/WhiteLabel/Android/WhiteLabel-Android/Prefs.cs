namespace WhiteLabelAndroid
{
    using Android.Content;
    using Android.Preferences;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;

    public class Prefs
    {
        private readonly ISharedPreferences prefs;
        private readonly Context context;

        private Prefs(Context context, ISharedPreferences sharedPreferences)
        {
            this.context = context;
            this.prefs = sharedPreferences;
        }

        public static Prefs From(Context context)
        {
            return new Prefs(context, PreferenceManager.GetDefaultSharedPreferences(context));
        }

        #region Instance URL
        public string GetInstanceUrl()
        {
            string instanceUrlKey = this.context.GetString(Resource.String.key_instance_url);
            string defaultInstanceUrl = this.context.GetString(Resource.String.text_default_instance_url);

            return this.GetString(instanceUrlKey, defaultInstanceUrl);
        }

        public void SaveInstanceUrl(string url)
        {
            this.PutString(this.context.GetString(Resource.String.key_instance_url), url);
        }
        #endregion Instance URL

        #region Login
        public string GetLogin()
        {
            string loginKey = this.context.GetString(Resource.String.key_login);
            string defaultLogin = this.context.GetString(Resource.String.text_default_login);

            return this.GetString(loginKey, defaultLogin);
        }

        public void SaveLogin(string login)
        {
            this.PutString(this.context.GetString(Resource.String.key_login), login);
        }
        #endregion Login

        #region Password
        public string GetPassword()
        {
            string passwordKey = this.context.GetString(Resource.String.key_password);
            string defaultPassword = this.context.GetString(Resource.String.text_default_password);

            return this.GetString(passwordKey, defaultPassword);
        }

        public void SavePassword(string password)
        {
            this.PutString(this.context.GetString(Resource.String.key_password), password);
        }
        #endregion Password

        #region Site
        public string GetSite()
        {
            string siteKey = this.context.GetString(Resource.String.key_site);
            string defaultSite = this.context.GetString(Resource.String.text_default_site);

            return this.GetString(siteKey, defaultSite);
        }

        public void SaveSite(string site)
        {
            this.PutString(this.context.GetString(Resource.String.key_site), site);
        }
        #endregion Site

        #region Database
        public string GetDatabase()
        {
            string databaseKey = this.context.GetString(Resource.String.key_database);
            string defaultDatabase = this.context.GetString(Resource.String.text_default_database);

            return this.GetString(databaseKey, defaultDatabase);
        }

        public void SaveDatabase(string database)
        {
            this.PutString(this.context.GetString(Resource.String.key_database), database);
        }
        #endregion Database

        public SessionConfig GetSessionConfig()
        {
            return new SessionConfig(this.GetInstanceUrl(), this.GetLogin(), this.GetPassword(), this.GetSite());
        }

        public ItemSource GetItemSource()
        {
            return new ItemSource(this.GetDatabase(), "en");
        }

        private string GetString(string key, string defaultValue)
        {
            return this.prefs.GetString(key, defaultValue);
        }

        private void PutString(string key, string value)
        {
            ISharedPreferencesEditor editor = this.prefs.Edit();
            editor.PutString(key, value);
            editor.Apply();
        }
    }
}