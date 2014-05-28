// WARNING
//
// This file has been generated automatically by Xamarin Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using System;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.CodeDom.Compiler;

namespace WhiteLabeliOS
{
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField dbField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField instanceUrlField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField loginField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField passwordField { get; set; }

		[Outlet]
		[GeneratedCode ("iOS Designer", "1.0")]
		MonoTouch.UIKit.UITextField siteField { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (dbField != null) {
				dbField.Dispose ();
				dbField = null;
			}
			if (instanceUrlField != null) {
				instanceUrlField.Dispose ();
				instanceUrlField = null;
			}
			if (loginField != null) {
				loginField.Dispose ();
				loginField = null;
			}
			if (passwordField != null) {
				passwordField.Dispose ();
				passwordField = null;
			}
			if (siteField != null) {
				siteField.Dispose ();
				siteField = null;
			}
		}
	}
}
