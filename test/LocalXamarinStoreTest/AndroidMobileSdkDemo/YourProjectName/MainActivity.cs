using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.API;
using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

namespace MobileSDKSample
{
  [Activity(Label = "YourProjectName", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    protected async override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      var endpoint = new SessionConfig("http://website-address", "username", "password", "/sitecore/shell"); 
      var defaultSource = ItemSource.DefaultSource();
      var session = new ScApiSession(endpoint, defaultSource);

      try
      {

        var request =
          ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
          .Payload(PayloadType.Content)
          .Build();
        var readHomeItemTask = session.ReadItemAsync(request);

        var items = await readHomeItemTask;

        string fieldText = items.Items[0].FieldWithName("Text").RawValue;
        string itemName = "Home Item Text";

        var dialogBuilder = new AlertDialog.Builder(this);
        dialogBuilder.SetTitle(itemName);
        dialogBuilder.SetMessage(fieldText);
        dialogBuilder.SetPositiveButton("OK", (object sender, DialogClickEventArgs e) =>
          {
          });

        dialogBuilder.Create().Show();
      }
      catch (Exception exception)
      {
        var dialogBuilder = new AlertDialog.Builder(this);
        dialogBuilder.SetTitle("error");
        dialogBuilder.SetMessage(exception.Message);
        dialogBuilder.SetPositiveButton("OK", (object sender, DialogClickEventArgs e) =>
          {
          });
        dialogBuilder.Create().Show();
      }
    }
  }
}


