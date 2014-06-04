namespace WhiteLabelAndroid
{
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Util;
    using Android.Views;
    using Android.Widget;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;

    [Activity(Label = "WhiteLabel-Android", MainLauncher = true)]
	public class MainActivity : Activity
    {
        private static readonly SessionConfig Config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "extranet\\creatorex", "creatorex");

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.main, menu);
            return base.OnCreateOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.settings:
                    this.StartActivity(new Intent(this, typeof(SettingsActivity)));
                    break;
            }

            return true;
        }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			this.SetContentView(Resource.Layout.Main);

			// Get our button from the layout resource,
			// and attach an event to it
			Button button = this.FindViewById<Button>(Resource.Id.myButton);

			button.Click += async (sender, e) =>
			{
				ScApiSession session = new ScApiSession(Config, ItemSource.DefaultSource());
//				await session.GetPublicKey();
//				Log.Error("some tag", "login : " + session.EncryptString("extranet\\creatorex"));
//				Log.Error("some tag", "pass : " + session.EncryptString("creatorex"));
			};
        }
    }
}