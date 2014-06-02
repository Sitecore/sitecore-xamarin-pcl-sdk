
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhiteLabeliOS
{
	public partial class UploadImageViewController : BaseTaskViewController
	{
		private UIImagePickerController imagePicker;

		public UploadImageViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("uploadImageVC", null);
		}

		partial void cancelUpload (MonoTouch.Foundation.NSObject sender)
		{
			AlertHelper.ShowAlertWithOkOption("Alert", "Not implemented yet");
			this.cancelButton.Enabled = false;
		}

		partial void startUpload (MonoTouch.Foundation.NSObject sender)
		{
			this.cancelButton.Enabled = true;
			this.chooseImageFromLibrary();
		}

		private void chooseImageFromLibrary()
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
					AlertHelper.ShowAlertWithOkOption("Alert", "Image uploading is not implemented yet");
				}
			} 
			else 
			{ 
				AlertHelper.ShowAlertWithOkOption("Alert", "Video uploading is not supported");
			}          

			imagePicker.DismissViewController (true, null);
		}

		void Handle_Canceled (object sender, EventArgs e) 
		{
			imagePicker.DismissViewController (true, null);
		}
	}
}

