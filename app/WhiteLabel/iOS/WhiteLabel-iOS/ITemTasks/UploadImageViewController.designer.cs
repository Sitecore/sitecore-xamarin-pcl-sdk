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
	[Register ("UploadImageViewController")]
	partial class UploadImageViewController
	{
		[Outlet]
		MonoTouch.UIKit.UIButton cancelButton { get; set; }

		[Outlet]
		MonoTouch.UIKit.UIProgressView uploadProgress { get; set; }

		[Action ("cancelUpload:")]
		partial void cancelUpload (MonoTouch.Foundation.NSObject sender);

		[Action ("startUpload:")]
		partial void startUpload (MonoTouch.Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (uploadProgress != null) {
				uploadProgress.Dispose ();
				uploadProgress = null;
			}

			if (cancelButton != null) {
				cancelButton.Dispose ();
				cancelButton = null;
			}
		}
	}
}
