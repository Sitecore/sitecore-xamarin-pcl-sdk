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
	[Register ("UploadImageViewController")]
	partial class UploadImageViewController
	{
		[Outlet]
		UIKit.UIButton cancelButton { get; set; }

		[Outlet]
		UIKit.UITextField itemNameTextField { get; set; }

		[Outlet]
		UIKit.UITextField itemPathTextField { get; set; }

		[Action ("OnCancelUploadButtonTouched:")]
		partial void OnCancelUploadButtonTouched (Foundation.NSObject sender);

		[Action ("OnUploadImageButtonTouched:")]
		partial void OnUploadImageButtonTouched (Foundation.NSObject sender);
		
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
