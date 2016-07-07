namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;
  using Sitecore.MobileSDK.API.Session;
  using Sitecore.MobileSDK.API;
  using System.IO;

  using Foundation;
  using UIKit;

	public partial class UploadImageViewController : BaseTaskViewController
	{
		private UIImagePickerController imagePicker;

		public UploadImageViewController(IntPtr handle) : base(handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("uploadImageVC", null);
		}

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      this.itemNameTextField.Placeholder = NSBundle.MainBundle.LocalizedString("Type item name", null);
      this.itemPathTextField.Placeholder = NSBundle.MainBundle.LocalizedString("Type item path", null);

    }

		partial void OnCancelUploadButtonTouched(Foundation.NSObject sender)
		{
			AlertHelper.ShowLocalizedNotImlementedAlert();
			this.cancelButton.Enabled = false;
		}

		partial void OnUploadImageButtonTouched(Foundation.NSObject sender)
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
				}
			} 
			else 
			{ 
				AlertHelper.ShowLocalizedAlertWithOkOption("Alert", "Video uploading is not supported");
			}          

			imagePicker.DismissViewController(true, null);
		}



		void Handle_Canceled(object sender, EventArgs e) 
		{
			imagePicker.DismissViewController(true, null);
		}
	}
}

