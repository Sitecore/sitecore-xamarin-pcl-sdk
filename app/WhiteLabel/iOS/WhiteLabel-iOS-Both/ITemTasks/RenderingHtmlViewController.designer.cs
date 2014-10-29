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
	[Register ("RenderingHtmlViewController")]
	partial class RenderingHtmlViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton getRenderingButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField renderingIdTextField { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIWebView resultWebView { get; set; }

		[Outlet]
		MonoTouch.UIKit.UITextField sourceIdTextField { get; set; }

		[Action ("OnGetRenderingTouch:")]
		partial void OnGetRenderingTouch (MonoTouch.Foundation.NSObject sender);
		
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
