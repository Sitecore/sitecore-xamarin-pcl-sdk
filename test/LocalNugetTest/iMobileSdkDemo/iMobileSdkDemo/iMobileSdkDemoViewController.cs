namespace iMobileSdkDemo
{
  using System;
  using System.Drawing;
  using System.Threading.Tasks;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;

  using SitecoreMobileSdkPasswordProvider.API;
  using SecureStringPasswordProvider.iOS;


  public partial class iMobileSdkDemoViewController : UIViewController
  {
    static bool UserInterfaceIdiomIsPhone
    {
      get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
    }

    public iMobileSdkDemoViewController(IntPtr handle) : base(handle)
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

