﻿namespace LargeUploadTestiOS
{
  using System;
  using System.IO;
  using System.Drawing;
  using System.Threading.Tasks;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using Sitecore.MobileSDK.API;
  using SitecoreMobileSdkPasswordProvider.API;
  using Sitecore.MobileSDK.API.Items;


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



      // iOS hardware : FileStream constructor throws
      // Access to the path "/var/mobile/Applications/2CD1D07E-26DD-43CC-AF52-F24368FB4676/LargeUploadTestiOS.app/IMG_0994.MOV" is denied.


      //      using (NSData movieContents = NSData.FromFile(resourceUrl))
      //      using (Stream videoOnFileSystem = movieContents.AsStream())
      using (IWebApiCredentials auth = new SecureStringPasswordProvider.iOS.SecureStringPasswordProvider("sitecore\\admin", "b"))
      using (var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(host)
        .Credentials(auth)
        .Site("/sitecore/shell")
        .BuildSession())

      {
        // TODO : dispose properly
        //        NSData movieContents = NSData.FromFile(resourceUrl);
        //        Stream videoOnFileSystem = movieContents.AsStream();


        byte[] tmp = System.Text.Encoding.UTF8.GetBytes("Hello World");
        Stream videoOnFileSystem = new MemoryStream(tmp);

        var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath("/")
          .ItemDataStream(videoOnFileSystem)
          .Database("master")
          .ItemName("NewLargeMedia for adk")
          .FileName("IMG_0997.MOV")
          //          .ContentType("video/quicktime")
          //          .ItemTemplatePath("System/Media/Unversioned/Movie")
          .Build();

        var response = await session.UploadMediaResourceAsync(request);
        return response;
      }
    }
  }
}



