using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sitecore.MobileSDK.SessionSettings;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.UrlBuilder.QueryParameters;
using System.Threading.Tasks;

namespace iMobileSdkDemo
{
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

      var endpoint = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:722", "admin", "b", "/sitecore/shell");
      var defaultSource = ItemSource.DefaultSource();
      var session = new ScApiSession(endpoint, defaultSource);

      var request =
        ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
          .Payload(PayloadType.Content)
          .Build();
      var readHomeItemTask = session.ReadItemAsync(request);

      ScItemsResponse items = await readHomeItemTask;
      string fieldText = items.Items[0].FieldWithName("Text").RawValue;

      UIAlertView alert = new UIAlertView("Home Item Text", fieldText, null, "OK", null);
      alert.Show();
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

