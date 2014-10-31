namespace iMobileSdkDemoUnified
{
  using System;
  using System.Drawing;

  using Foundation;
  using UIKit;
  using Sitecore.MobileSDK.PasswordProvider.iOS;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;



  public partial class iMobileSdkDemo_UnifiedViewController : UIViewController
  {
    public iMobileSdkDemo_UnifiedViewController(IntPtr handle) : base(handle)
    {
    }

    public override void DidReceiveMemoryWarning()
    {
      // Releases the view if it doesn't have a superview.
      base.DidReceiveMemoryWarning();
            
      // Release any cached data, images, etc that aren't in use.
    }

    #region View lifecycle

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
            
      // Perform any additional setup after loading the view, typically from a nib.
    }

    public override void ViewWillAppear(bool animated)
    {
      base.ViewWillAppear(animated);
    }

    public override async void ViewDidAppear(bool animated)
    {
      base.ViewDidAppear(animated);

      // first we have to setup connection info and create a session
      var instanceUrl = "http://mobiledev1ua1.dk.sitecore.net:722";

      using (var credentials = new SecureStringPasswordProvider("admin", "b"))
      using 
        (
          var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(instanceUrl)
          .Credentials(credentials)
          .WebApiVersion("v1")
          .DefaultDatabase("web")
          .DefaultLanguage("en")
          .MediaLibraryRoot("/sitecore/media library")
          .MediaPrefix("~/media/")
          .DefaultMediaResourceExtension("ashx")
          .BuildSession()
        )
      {
        // In order to fetch some data we have to build a request
        var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
          .AddFieldsToRead("text")
          .AddScope(ScopeType.Self)
          .Build();

        // And execute it on a session asynchronously
        var response = await session.ReadItemAsync(request);

        // Now that it has succeeded we are able to access downloaded items
        ISitecoreItem item = response[0];

        // And content stored it its fields
        string fieldContent = item["text"].RawValue;

        UIAlertView alert = new UIAlertView("Sitecore SDK Demo", fieldContent, null, "Ok", null);
        alert.Show();
      }
    }

    public override void ViewWillDisappear(bool animated)
    {
      base.ViewWillDisappear(animated);
    }

    public override void ViewDidDisappear(bool animated)
    {
      base.ViewDidDisappear(animated);
    }

    #endregion
  }
}

