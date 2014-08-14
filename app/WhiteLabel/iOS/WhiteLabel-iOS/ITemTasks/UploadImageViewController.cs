
namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

	public partial class UploadImageViewController : BaseTaskViewController
	{
		private UIImagePickerController imagePicker;

		public UploadImageViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("uploadImageVC", null);
		}

		partial void OnCancelUploadButtonTouched (MonoTouch.Foundation.NSObject sender)
		{
			AlertHelper.ShowLocalizedNotImlementedAlert();
			this.cancelButton.Enabled = false;
		}

		partial void OnUploadImageButtonTouched (MonoTouch.Foundation.NSObject sender)
		{
			this.cancelButton.Enabled = true;
			this.ChooseImageFromLibrary();
		}

		private void ChooseImageFromLibrary()
		{
		    imagePicker = new UIImagePickerController ();
			imagePicker.SourceType = UIImagePickerControllerSourceType.PhotoLibrary;
			imagePicker.MediaTypes = UIImagePickerController.AvailableMediaTypes (UIImagePickerControllerSourceType.PhotoLibrary);
			imagePicker.FinishedPickingMedia += Handle_FinishedPickingMedia;
			imagePicker.Canceled += Handle_Canceled;
			this.NavigationController.PresentViewController (imagePicker, true, null);
		}

		protected void Handle_FinishedPickingMedia (object sender, UIImagePickerMediaPickedEventArgs e)
		{
			bool isImage = false;
			switch(e.Info[UIImagePickerController.MediaType].ToString()) {
			case "public.image":
				Console.WriteLine("Image selected");
				isImage = true;
				break;
			case "public.video":
				Console.WriteLine("Video selected");
				break;
			}

			if(isImage) 
			{

				UIImage originalImage = e.Info[UIImagePickerController.OriginalImage] as UIImage;
				if(originalImage != null) 
				{
					AlertHelper.ShowLocalizedNotImlementedAlert();
				}
			} 
			else 
			{ 
				AlertHelper.ShowLocalizedAlertWithOkOption("Alert", "Video uploading is not supported");
			}          

			imagePicker.DismissViewController (true, null);
		}

		void Handle_Canceled (object sender, EventArgs e) 
		{
			imagePicker.DismissViewController (true, null);
		}
	}
}

