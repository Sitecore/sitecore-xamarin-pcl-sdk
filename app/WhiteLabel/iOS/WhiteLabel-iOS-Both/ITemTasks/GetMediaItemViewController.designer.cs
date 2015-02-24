// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
#if __UNIFIED__
using UIKit;
using Foundation;
#else
using MonoTouch.UIKit;
using MonoTouch.Foundation;
#endif


using System.CodeDom.Compiler;

namespace WhiteLabeliOS
{
	[Register ("GetMediaItemViewController")]
	partial class GetMediaItemViewController
	{
		[Outlet]
		UIButton DownloadButton { get; set; }

		[Outlet]
		UILabel HeightLabel { get; set; }

		[Outlet]
		UITextField HeightTextField { get; set; }

		[Outlet]
		UIImageView ImageView { get; set; }

		[Outlet]
		UITextField MediaPathTextField { get; set; }

		[Outlet]
		UILabel WidthLabel { get; set; }

		[Outlet]
		UITextField WidthTextField { get; set; }

		[Action ("OnDownloadButtonTouched:")]
		partial void OnDownloadButtonTouched (NSObject sender);
		
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
