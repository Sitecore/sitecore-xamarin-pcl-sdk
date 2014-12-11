// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WhiteLabeliOS
{
	[Register ("GetMediaItemViewController")]
	partial class GetMediaItemViewController
	{
		[Outlet]
		UIKit.UIButton DownloadButton { get; set; }

		[Outlet]
		UIKit.UILabel HeightLabel { get; set; }

		[Outlet]
		UIKit.UITextField HeightTextField { get; set; }

		[Outlet]
		UIKit.UIImageView ImageView { get; set; }

		[Outlet]
		UIKit.UITextField MediaPathTextField { get; set; }

		[Outlet]
		UIKit.UILabel WidthLabel { get; set; }

		[Outlet]
		UIKit.UITextField WidthTextField { get; set; }

		[Action ("OnDownloadButtonTouched:")]
		partial void OnDownloadButtonTouched (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (WidthLabel != null) {
				WidthLabel.Dispose ();
				WidthLabel = null;
			}

			if (HeightLabel != null) {
				HeightLabel.Dispose ();
				HeightLabel = null;
			}

			if (DownloadButton != null) {
				DownloadButton.Dispose ();
				DownloadButton = null;
			}

			if (HeightTextField != null) {
				HeightTextField.Dispose ();
				HeightTextField = null;
			}

			if (ImageView != null) {
				ImageView.Dispose ();
				ImageView = null;
			}

			if (MediaPathTextField != null) {
				MediaPathTextField.Dispose ();
				MediaPathTextField = null;
			}

			if (WidthTextField != null) {
				WidthTextField.Dispose ();
				WidthTextField = null;
			}
		}
	}
}
