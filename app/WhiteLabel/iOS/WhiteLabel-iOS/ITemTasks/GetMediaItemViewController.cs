
namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;
  using System.IO;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.API.MediaItem;
  using Sitecore.MobileSDK.API;

  public partial class GetMediaItemViewController : BaseTaskViewController
  {
    public GetMediaItemViewController(IntPtr handle) : base (handle)
    {
      Title = NSBundle.MainBundle.LocalizedString("getMediaItem", null);
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.MediaPathTextField.Placeholder = NSBundle.MainBundle.LocalizedString("Type Media Path", null);

      string ButtonTitle = NSBundle.MainBundle.LocalizedString("Download", null);
      this.DownloadButton.SetTitle (ButtonTitle, UIControlState.Normal);

      this.HeightLabel.Text = NSBundle.MainBundle.LocalizedString("Height:", null);
      this.WidthLabel.Text = NSBundle.MainBundle.LocalizedString("Width:", null);

      this.MediaPathTextField.Text = "/sitecore/media library/fffffffff";
    }

    public override void ViewDidAppear(bool animated)
    {
      base.ViewDidAppear(animated);

      this.maxWidth = (int)Math.Round(this.ImageView.Bounds.Width);
      this.maxHeight = (int)Math.Round(this.ImageView.Bounds.Height);

      this.WidthTextField.Text = maxWidth.ToString();
      this.HeightTextField.Text = maxHeight.ToString();
    }

    partial void OnDownloadButtonTouched(MonoTouch.Foundation.NSObject sender)
    {
      try
      {
        if (String.IsNullOrEmpty(this.WidthTextField.Text)
          ||String.IsNullOrEmpty(this.HeightTextField.Text))
        {
          AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Incorect width or height value");
          return;
        }

        this.width = Convert.ToInt32(this.WidthTextField.Text);
        this.height = Convert.ToInt32(this.HeightTextField.Text);

        if (String.IsNullOrEmpty(this.MediaPathTextField.Text))
        {
          AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type media path");
        }
        else
        {
          bool wrongSizeData = this.width<=0
            ||this.width>this.maxWidth
            ||this.height<=0
            ||this.height>this.maxHeight;

          if (wrongSizeData)
          {
            AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Incorect width or height value");
          }
          else
          {
            this.HideKeyboard(this.MediaPathTextField);
            this.HideKeyboard(this.WidthTextField);
            this.HideKeyboard(this.HeightTextField);

            this.SendRequest();
          }
        }
      }
      catch(Exception e) 
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", e.Message);
      }
    }

    private async void SendRequest ()
    {
      try
      {
        using (ISitecoreWebApiSession session = this.instanceSettings.GetSession())
        {
          IDownloadMediaOptions options = new MediaOptionsBuilder()
            .Set
            .Width(this.width)
            .Height(this.height)
            .BackgroundColor("white")
            .Build();

          string path = this.MediaPathTextField.Text;

          var request = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(path)
            .DownloadOptions(options)
            .Build();


          byte[] data = null;
          using (Stream response = await session.DownloadMediaResourceAsync(request))
          using (MemoryStream responseInMemory = new MemoryStream())
          {
            await response.CopyToAsync(responseInMemory);
            data = responseInMemory.ToArray();
          }

          BeginInvokeOnMainThread(delegate
          {
            using ( UIImage image = new UIImage(NSData.FromArray(data)) )
            {
              // no need disposing 
              // since this.ImageView.Image creates a 
              // new C# object on each call
              this.ImageView.Image = image;

              // Update Overlay
              this.HideLoader();
            }
          });
        }
      }
      catch(Exception e) 
      {
        AlertHelper.ShowLocalizedAlertWithOkOption("Error", e.Message);
      }
      finally
      {
        BeginInvokeOnMainThread(delegate
        {
          this.HideLoader();
        });
      }
    }

    public int width;
    public int height;

    public int maxWidth;
    public int maxHeight;
  }
}

