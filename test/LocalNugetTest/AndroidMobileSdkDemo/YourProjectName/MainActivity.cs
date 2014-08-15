using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Sitecore.MobileSDK.API.Session;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK.API;
using Sitecore.MobileSDK.API.Request.Parameters;
using Sitecore.MobileSDK.API.Items;

namespace MobileSDKSample
{
  [Activity(Label = "YourProjectName", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    protected async override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      string instanceUrl = "http://my.site.com";
      var session = 
        SitecoreWebApiSessionBuilder.AnonymousSessionWithHost(instanceUrl)
          .Site("/sitecore/shell")
          .DefaultDatabase("web")
          .DefaultLanguage("en")
          .MediaLibraryRoot("/sitecore/media library")
          .MediaPrefix("~/media/")
          .DefaultMediaResourceExtension("ashx")
          .BuildReadonlySession();

      var request =
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
          .AddScope(ScopeType.Self)
          .Payload(PayloadType.Content)
          .Build();
      var response = await session.ReadItemAsync(request);

      // Now that it has succeeded we are able to access downloaded items
      ISitecoreItem item = response[0];

      // And content stored it its fields
      string fieldContent = item["text"].RawValue;

      string itemName = "Home Item Text";

      var dialogBuilder = new AlertDialog.Builder(this);
      dialogBuilder.SetTitle(itemName);
      dialogBuilder.SetMessage(fieldContent);
      dialogBuilder.SetPositiveButton("OK", (object sender, DialogClickEventArgs e) =>
        {
        });

      dialogBuilder.Create().Show();

    }
  }
}


