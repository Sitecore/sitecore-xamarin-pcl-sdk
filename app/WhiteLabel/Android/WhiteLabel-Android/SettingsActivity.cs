namespace WhiteLabelAndroid
{
    using Android.App;
    using Android.OS;
    using Android.Widget;

    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        private Prefs prefs;

        private EditText instanceUrl;
        private EditText login;
        private EditText password;
        private EditText site;
        private EditText database;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.Settings);
            
            this.prefs = Prefs.From(this);

            this.InitFields();

            Button useButton = this.FindViewById<Button>(Resource.Id.button_use);

            useButton.Click += (sender, args) =>
            {
                this.SaveFieldsToPrefs();
                Toast.MakeText(this, "Saved instance", ToastLength.Short).Show();
            };
        }

        private void SaveFieldsToPrefs()
        {
            this.prefs.PutString(this.GetString(Resource.String.key_instance_url), this.instanceUrl.Text);
            this.prefs.PutString(this.GetString(Resource.String.key_login), this.login.Text);
            this.prefs.PutString(this.GetString(Resource.String.key_password), this.password.Text);
            this.prefs.PutString(this.GetString(Resource.String.key_site), this.site.Text);
            this.prefs.PutString(this.GetString(Resource.String.key_database), this.database.Text);
        }

        private void InitFields()
        {
            this.instanceUrl = this.FindViewById<EditText>(Resource.Id.instance_url);
            this.login = this.FindViewById<EditText>(Resource.Id.instance_login);
            this.password = this.FindViewById<EditText>(Resource.Id.instance_password);
            this.site = this.FindViewById<EditText>(Resource.Id.instance_site);
            this.database = this.FindViewById<EditText>(Resource.Id.instance_database);

            this.instanceUrl.Text = this.prefs.GetString(this.GetString(Resource.String.key_instance_url), "http://mobiledev1ua1.dk.sitecore.net:722");
            this.login.Text = this.prefs.GetString(this.GetString(Resource.String.key_login), "extranet\\creatorex");
            this.password.Text = this.prefs.GetString(this.GetString(Resource.String.key_password), "creatorex");
            this.site.Text = this.prefs.GetString(this.GetString(Resource.String.key_site), string.Empty);
            this.database.Text = this.prefs.GetString(this.GetString(Resource.String.key_database), "web");
        }

    }
}

