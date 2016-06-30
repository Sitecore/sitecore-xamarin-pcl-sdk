﻿using System;
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
using Sitecore.MobileSDK.API.Request.Parameters;
using Sitecore.MobileSDK.API.Items;

using Sitecore.MobileSDK.PasswordProvider.Android;

namespace AndroidMobileSdkDemo
{
  [Activity(Label = "AndroidMobileSdkDemo", MainLauncher = true, Icon = "@drawable/icon")]
  public class MainActivity : Activity
  {
    protected async override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);

      #warning first we have to setup connection info and create a session
      string instanceUrl = "http://myinstance.com/";

      using (var credentials = new SecureStringPasswordProvider("login", "password"))
      using (
        var session =
          SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(instanceUrl)
        .Credentials(credentials)
        .DefaultDatabase("web")
        .DefaultLanguage("en")
        .BuildReadonlySession())
      {
        var request =
          ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
            .AddScope(ScopeType.Self)
            .Payload(PayloadType.Content)
            .Build();

        ScItemsResponse items = await session.ReadItemAsync(request);
        string fieldContent = items[0]["Text"].RawValue;

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
}


