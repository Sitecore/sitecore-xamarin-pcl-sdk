namespace WhiteLabelAndroid
{
    using Android.App;
    using Android.OS;
    using Android.Widget;

    [Activity(Label = "SettingsActivity")]
    public class SettingsActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            this.SetContentView(Resource.Layout.Settings);


            Button useButton = this.FindViewById<Button>(Resource.Id.button_use);

            useButton.Click += (sender, args) =>
            {
                EditText instanceUrl = this.FindViewById<EditText>(Resource.Id.instance_url);
                EditText login = this.FindViewById<EditText>(Resource.Id.instance_login);
                EditText password = this.FindViewById<EditText>(Resource.Id.instance_password);
                EditText site = this.FindViewById<EditText>(Resource.Id.instance_site);
                EditText database = this.FindViewById<EditText>(Resource.Id.instance_database);

                Toast.MakeText(this, "Saved instance", ToastLength.Short).Show();
            };
        }

    }
}

