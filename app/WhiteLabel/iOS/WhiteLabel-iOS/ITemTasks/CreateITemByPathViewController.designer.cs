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
	[Register ("CreateITemByPathViewController")]
	partial class CreateITemByPathViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton createButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField nameField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField pathField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField textField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField titleField { get; set; }

		[Action ("OnCreateItemButtonTapped:")]
		partial void OnCreateItemButtonTapped (MonoTouch.UIKit.UIButton sender);

		[Action ("OnUpdateItemButtonTapped:")]
		partial void OnUpdateItemButtonTapped (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (createButton != null) {
				createButton.Dispose ();
				createButton = null;
			}

			if (nameField != null) {
				nameField.Dispose ();
				nameField = null;
			}

			if (pathField != null) {
				pathField.Dispose ();
				pathField = null;
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
