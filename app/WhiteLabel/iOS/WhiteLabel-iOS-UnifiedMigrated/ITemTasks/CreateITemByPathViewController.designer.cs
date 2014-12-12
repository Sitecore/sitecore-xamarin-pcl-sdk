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
	[Register ("CreateITemByPathViewController")]
	partial class CreateITemByPathViewController
	{
		[Outlet]
		UIKit.UIButton createButton { get; set; }

		[Outlet]
		UIKit.UITextField nameField { get; set; }

		[Outlet]
		UIKit.UITextField pathField { get; set; }

		[Outlet]
		UIKit.UITextField textField { get; set; }

		[Outlet]
		UIKit.UITextField titleField { get; set; }

		[Outlet]
		UIKit.UIButton updateButton { get; set; }

		[Action ("OnCreateItemButtonTapped:")]
		partial void OnCreateItemButtonTapped (UIKit.UIButton sender);

		[Action ("OnUpdateItemButtonTapped:")]
		partial void OnUpdateItemButtonTapped (UIKit.UIButton sender);
		
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

			if (updateButton != null) {
				updateButton.Dispose ();
				updateButton = null;
			}
		}
	}
}
