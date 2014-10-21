using System;
using System.Drawing;

using Foundation;
using UIKit;
using System.IO;
using Sitecore.MobileSDK.API;
using SecureStringPasswordProvider.API;
using System.Threading.Tasks;
using SitecoreMobileSdkPasswordProvider.API;
using Sitecore.MobileSDK.API.Items;

namespace LargeUploadTestiOS
{
  public partial class LargeUploadTest_iOSViewController : UIViewController
  {
    static bool UserInterfaceIdiomIsPhone
    {
      get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
    }

    public LargeUploadTest_iOSViewController(IntPtr handle) : base(handle)
    {
    }

    public override void DidReceiveMemoryWarning()
    {
      // Releases the view if it doesn't have a superview.
      base.DidReceiveMemoryWarning();
      Console.WriteLine("!!!! MEMORY WARNING !!!!");            
      // Release any cached data, images, etc that aren't in use.
    }
      
    public override async void ViewDidLoad()
    {
      base.ViewDidLoad();

      var uploadResult = await this.UploadLargeImage();
      UIAlertView alert = new UIAlertView("title", "uploaded", null, "Cool!", null);
      alert.Show();
    }

    private async Task<ScItemsResponse> UploadLargeImage()
    {
      var resourceUrl = NSBundle.MainBundle.PathForResource("IMG_0994", "MOV");
      string host = "http://cms71u3.test24dk1.dk.sitecore.net";

      using (Stream videoOnFileSystem = new FileStream(resourceUrl, FileMode.Open))
      using (IWebApiCredentials auth = new SecureStringPasswordProvider.API.SecureStringPasswordProvider("sitecore\\admin", "b"))
      using (var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(host)
                                                       .Credentials(auth)
                                                       .Site("/sitecore/shell")
                                                       .BuildSession())

      {
        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath("/")
          .ItemDataStream(videoOnFileSystem)
          .Database("master")
          .ItemName("NewLargeMedia for adk")
          .FileName("IMG_0997.MOV")
          .ContentType("video/quicktime")
          .ItemTemplatePath("System/Media/Unversioned/Movie")
          .Build();

        var response = await session.UploadMediaResourceAsync(request);
        return response;
      }
    }
  }
}

