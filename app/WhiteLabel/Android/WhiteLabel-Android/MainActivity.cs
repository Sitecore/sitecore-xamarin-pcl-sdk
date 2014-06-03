namespace WhiteLabelAndroid
{
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Util;
    using Android.Views;
    using Android.Widget;
    using FormsApp;
    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.SessionSettings;
    using Xamarin.Forms.Platform.Android;

    [Activity(Label = "WhiteLabel-Android", MainLauncher = true)]
    public class MainActivity : AndroidActivity
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

            Xamarin.Forms.Forms.Init(this, bundle);

            this.SetPage(App.GetMainPage());
        }
    }
}