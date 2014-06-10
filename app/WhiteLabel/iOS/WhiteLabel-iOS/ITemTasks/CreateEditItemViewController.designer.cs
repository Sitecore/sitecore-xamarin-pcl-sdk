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
	[Register ("CreateEditItemViewController")]
	partial class CreateEditItemViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITextField pathField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIButton saveButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField textField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField titleField { get; set; }

		[Action ("OnCreateItemButtonTapped:")]
		partial void OnCreateItemButtonTapped (MonoTouch.Foundation.NSObject sender);

		[Action ("OnSaveItemButtonTapped:")]
		partial void OnSaveItemButtonTapped (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (pathField != null) {
				pathField.Dispose ();
				pathField = null;
			}

			if (saveButton != null) {
				saveButton.Dispose ();
				saveButton = null;
			}

			if (textField != null) {
				textField.Dispose ();
				textField = null;
			}

			if (titleField != null) {
				titleField.Dispose ();
				titleField = null;
			}
		}
	}
}
