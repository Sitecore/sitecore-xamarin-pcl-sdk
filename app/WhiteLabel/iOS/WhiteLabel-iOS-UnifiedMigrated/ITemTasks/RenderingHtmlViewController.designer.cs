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
	[Register ("RenderingHtmlViewController")]
	partial class RenderingHtmlViewController
	{
		[Outlet]
		UIKit.UIButton getRenderingButton { get; set; }

		[Outlet]
		UIKit.UITextField renderingIdTextField { get; set; }

		[Outlet]
		UIKit.UIWebView resultWebView { get; set; }

		[Outlet]
		UIKit.UITextField sourceIdTextField { get; set; }

		[Action ("OnGetRenderingTouch:")]
		partial void OnGetRenderingTouch (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (sourceIdTextField != null) {
				sourceIdTextField.Dispose ();
				sourceIdTextField = null;
			}

			if (renderingIdTextField != null) {
				renderingIdTextField.Dispose ();
				renderingIdTextField = null;
			}

			if (getRenderingButton != null) {
				getRenderingButton.Dispose ();
				getRenderingButton = null;
			}

			if (resultWebView != null) {
				resultWebView.Dispose ();
				resultWebView = null;
			}
		}
	}
}
