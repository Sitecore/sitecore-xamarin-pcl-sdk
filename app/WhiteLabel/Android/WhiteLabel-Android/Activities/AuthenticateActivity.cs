namespace WhiteLabelAndroid.Activities
{
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  using Sitecore.MobileSDK.PasswordProvider.Android;
  using Sitecore.MobileSDK.API;

  [Activity]
  public class AuthenticateActivity : Activity
  {
    private EditText instanceUrlField;
    private EditText loginField;
    private EditText passwordField;
    private EditText siteField;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      this.SetContentView(Resource.Layout.activity_settings);

      this.SetTitle(Resource.String.text_authenticate);

      var database = this.FindViewById<EditText>(Resource.Id.field_instance_database);
      database.Visibility = ViewStates.Gone;

      this.instanceUrlField = this.FindViewById<EditText>(Resource.Id.field_instance_url);
      this.loginField = this.FindViewById<EditText>(Resource.Id.field_instance_login);
      this.passwordField = this.FindViewById<EditText>(Resource.Id.field_instance_password);
      this.siteField = this.FindViewById<EditText>(Resource.Id.field_instance_site);

      var authenticateButton = this.FindViewById<Button>(Resource.Id.button_use);

      authenticateButton.Click += (sender, args) => this.Authenticate();
    }

    private async void Authenticate()
    {
      var instanceUrl = this.instanceUrlField.Text;
      var login = this.loginField.Text;
      var password = this.passwordField.Text;
      var site = this.siteField.Text;

      if (string.IsNullOrEmpty(instanceUrl))
      {
        Toast.MakeText(this, "Please provide instance url", ToastLength.Long).Show();
        return;
      }

      if (string.IsNullOrEmpty(login) && string.IsNullOrEmpty(password))
      {
        Toast.MakeText(this, "Please provide both login and password", ToastLength.Long).Show();
        return;
      }

      try
      {
        using (var credentials = new SecureStringPasswordProvider(login, password))
          using (var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(instanceUrl)
            .Credentials(credentials)
            .Site(site)
            .BuildReadonlySession())
          {
            this.SetProgressBarIndeterminateVisibility(true);

            bool response = await session.AuthenticateAsync();

            this.SetProgressBarIndeterminateVisibility(false);
            if (response)
            {
              DialogHelper.ShowSimpleDialog(this, "Success", "This user exists");
            }
            else
            {
              DialogHelper.ShowSimpleDialog(this, "Failed", "This user doesn't exist");
            }
          }
      }
      catch (System.Exception exception)
      {
        this.SetProgressBarIndeterminateVisibility(false);

        var title = this.GetString(Resource.String.text_error);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }
  }
}