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
	[Register ("SettingsViewController")]
	partial class SettingsViewController
	{
		[Outlet]
		UIKit.UITextField dbField { get; set; }

		[Outlet]
		UIKit.UITextField instanceUrlField { get; set; }

		[Outlet]
		UIKit.UITextField languageField { get; set; }

		[Outlet]
		UIKit.UITextField loginField { get; set; }

		[Outlet]
		UIKit.UITextField passwordField { get; set; }

		[Outlet]
		UIKit.UITextField siteField { get; set; }
		
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

			if (languageField != null) {
				languageField.Dispose ();
				languageField = null;
			}
		}
	}
}
