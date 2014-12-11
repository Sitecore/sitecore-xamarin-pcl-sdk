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
	[Register ("UploadImageViewController")]
	partial class UploadImageViewController
	{
		[Outlet]
		UIButton cancelButton { get; set; }

		[Outlet]
		UITextField itemNameTextField { get; set; }

		[Outlet]
		UITextField itemPathTextField { get; set; }

		[Action ("OnCancelUploadButtonTouched:")]
		partial void OnCancelUploadButtonTouched (NSObject sender);

		[Action ("OnUploadImageButtonTouched:")]
		partial void OnUploadImageButtonTouched (NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (itemPathTextField != null) {
				itemPathTextField.Dispose ();
				itemPathTextField = null;
			}

			if (itemNameTextField != null) {
				itemNameTextField.Dispose ();
				itemNameTextField = null;
			}

			if (cancelButton != null) {
				cancelButton.Dispose ();
				cancelButton = null;
			}
		}
	}
}
