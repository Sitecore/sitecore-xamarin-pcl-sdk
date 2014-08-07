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
	[Register ("AuthViewController")]
	partial class AuthViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton authButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField loginField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField passwordField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField urlField { get; set; }

		[Action ("OnAuthButtonTapped:")]
		partial void OnAuthButtonTapped (MonoTouch.UIKit.UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (authButton != null) {
				authButton.Dispose ();
				authButton = null;
			}

			if (loginField != null) {
				loginField.Dispose ();
				loginField = null;
			}

			if (passwordField != null) {
				passwordField.Dispose ();
				passwordField = null;
			}

			if (urlField != null) {
				urlField.Dispose ();
				urlField = null;
			}
		}
	}
}
