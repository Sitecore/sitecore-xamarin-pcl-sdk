// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using MonoTouch.Foundation;
using System.CodeDom.Compiler;

namespace WhiteLabeliOS
{
	[Register ("GetMediaItemViewController")]
	partial class GetMediaItemViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton DownloadButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIImageView ImageView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField MediaPathTextField { get; set; }

		[Action ("OnDownloadButtonTouched:")]
		partial void OnDownloadButtonTouched (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (MediaPathTextField != null) {
				MediaPathTextField.Dispose ();
				MediaPathTextField = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (DownloadButton != null) {
				DownloadButton.Dispose ();
				DownloadButton = null;
			}
		}
	}
}
