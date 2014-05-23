namespace WhiteLabelAndroid
{
    using System;
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Runtime;
    using Android.Util;
    using Android.Views;
    using Android.Widget;
    using Sitecore.MobileSDK;

    [Activity(Label = "WhiteLabel-Android", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private static readonly SessionConfig Config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:7119", "extranet\\creatorex", "creatorex");
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            this.SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.myButton);

            button.Click += async (sender, e) =>
            {
                ScApiSession session = new ScApiSession(Config);
                await session.GetPublicKey();
                Log.Error("some tag", "login : " + session.EncryptString("extranet\\creatorex"));
                Log.Error("some tag", "pass : " + session.EncryptString("creatorex"));
            };
        }
    }
}