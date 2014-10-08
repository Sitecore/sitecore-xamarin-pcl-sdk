namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API;
  using System.IO;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

	public partial class UploadImageViewController : BaseTaskViewController
	{
		private UIImagePickerController imagePicker;

		public UploadImageViewController(IntPtr handle) : base(handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("uploadImageVC", null);
		}

		partial void OnCancelUploadButtonTouched(MonoTouch.Foundation.NSObject sender)
		{
			AlertHelper.ShowLocalizedNotImlementedAlert();
			this.cancelButton.Enabled = false;
		}

		partial void OnUploadImageButtonTouched(MonoTouch.Foundation.NSObject sender)
		{
			this.cancelButton.Enabled = true;
			this.ChooseImageFromLibrary();
		}

		private void ChooseImageFromLibrary()
		{
      this.imagePicker = new UIImagePickerController();
      this.imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
      this.imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes(UIImagePickerControllerSourceType.PhotoLibrary);
      this.imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
      this.imagePicker.Canceled += Handle_Canceled;
			this.NavigationController.PresentViewController(imagePicker, true, null);
		}

		protected void Handle_FinishedPickingMedia(object sender, UIImagePickerMediaPickedEventArgs e)
		{
			bool isImage = false;
			switch(e.Info[UIImagePickerController.MediaType].ToString()) 
      {
        case "public.image":
        {
          Console.WriteLine("Image selected");
          isImage = true;
          break;
        }
        case "public.video":
        {
          Console.WriteLine("Video selected");
          break;
        }
			}

			if (isImage) 
			{
				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if(originalImage != null) 
				{
          this.SendImage(originalImage);
				}
			} 
			else 
			{ 
				AlertHelper.ShowLocalizedAlertWithOkOption("Alert", "Video uploading is not supported");
			}          

			imagePicker.DismissViewController(true, null);
		}

    private async void SendImage(UIImage image)
    {
      try
      {
        using (ISitecoreWebApiSession session = this.instanceSettings.GetSession())
        {
          Stream stream = image.AsJPEG().AsStream();
          var request = ItemWebApiRequestBuilder.UploadResourceRequest(stream)
            .ContentType("image/jpg")
            .ItemName("name1")
            .FileName("bugaga.jpg")
            .ItemTemplate("System/Media/Unversioned/Image")
            .MediaPath("")
            .Build();

          this.ShowLoader();

          var response = await session.UploadMediaResourceAsync(request);

          if (response != null)
          {
            AlertHelper.ShowAlertWithOkOption("upload image result","OK");
          }
          else
          {
            AlertHelper.ShowAlertWithOkOption("upload image result","something wrong");
          }
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

		void Handle_Canceled(object sender, EventArgs e) 
		{
			imagePicker.DismissViewController(true, null);
		}
	}
}

