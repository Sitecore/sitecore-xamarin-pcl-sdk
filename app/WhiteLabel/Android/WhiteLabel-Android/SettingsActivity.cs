namespace WhiteLabelAndroid
{
  using Android.App;
  using Android.Content.PM;
  using Android.OS;
  using Android.Widget;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
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
      this.SetTitle(Resource.String.text_settings_screen);
      this.prefs = Prefs.From(this);

      this.InitFields();

      Button useButton = this.FindViewById<Button>(Resource.Id.button_use);

      useButton.Click += (sender, args) =>
      {
        this.SaveFieldsToPrefs();
        Toast.MakeText(this, GetString(Resource.String.text_instance_saved), ToastLength.Short).Show();
        this.Finish();
      };
    }

    private void SaveFieldsToPrefs()
    {
      this.prefs.InstanceUrl = this.instanceUrl.Text;
      this.prefs.Login = this.login.Text;
      this.prefs.Password = this.password.Text;
      this.prefs.Site = this.site.Text;
      this.prefs.Database = this.database.Text;
    }

    private void InitFields()
    {
      this.instanceUrl = this.FindViewById<EditText>(Resource.Id.instance_url);
      this.login = this.FindViewById<EditText>(Resource.Id.instance_login);
      this.password = this.FindViewById<EditText>(Resource.Id.instance_password);
      this.site = this.FindViewById<EditText>(Resource.Id.instance_site);
      this.database = this.FindViewById<EditText>(Resource.Id.instance_database);

      this.instanceUrl.Text = this.prefs.InstanceUrl;
      this.login.Text = this.prefs.Login;
      this.password.Text = this.prefs.Password;
      this.site.Text = this.prefs.Site;
      this.database.Text = this.prefs.Database;
    }
  }
}

