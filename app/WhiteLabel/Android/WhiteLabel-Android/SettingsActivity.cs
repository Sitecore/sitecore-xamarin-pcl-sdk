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
                this.Finish();
            };
        }

        private void SaveFieldsToPrefs()
        {
            this.prefs.SaveInstanceUrl(this.instanceUrl.Text);
            this.prefs.SaveLogin(this.login.Text);
            this.prefs.SavePassword(this.password.Text);
            this.prefs.SaveSite(this.site.Text);
            this.prefs.SaveDatabase(this.database.Text);
        }

        private void InitFields()
        {
            this.instanceUrl = this.FindViewById<EditText>(Resource.Id.instance_url);
            this.login = this.FindViewById<EditText>(Resource.Id.instance_login);
            this.password = this.FindViewById<EditText>(Resource.Id.instance_password);
            this.site = this.FindViewById<EditText>(Resource.Id.instance_site);
            this.database = this.FindViewById<EditText>(Resource.Id.instance_database);

            this.instanceUrl.Text = this.prefs.GetInstanceUrl();
            this.login.Text = this.prefs.GetLogin();
            this.password.Text = this.prefs.GetPassword();
            this.site.Text = this.prefs.GetSite();
            this.database.Text = this.prefs.GetDatabase();
        }
    }
}

