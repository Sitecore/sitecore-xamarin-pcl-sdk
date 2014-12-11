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
	[Register ("DeleteITemByIdViewController")]
	partial class DeleteITemByIdViewController
	{
		[Outlet]
		UIButton deleteByIdButton { get; set; }

		[Outlet]
		UIButton deleteByPathButton { get; set; }

		[Outlet]
		UIButton deleteByQueryButton { get; set; }

		[Outlet]
		UITextField itemIdField { get; set; }

		[Outlet]
		UITextField itemPathField { get; set; }

		[Outlet]
		UITextField itemQueryField { get; set; }

		[Action ("OnDeleteItemByIdButtonTouched:")]
		partial void OnDeleteItemByIdButtonTouched (UIButton sender);

		[Action ("OnDeleteItemByPathButtonTouched:")]
		partial void OnDeleteItemByPathButtonTouched (UIButton sender);

		[Action ("OnDeleteItemByqueryButtonTouched:")]
		partial void OnDeleteItemByqueryButtonTouched (UIButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (itemIdField != null) {
				itemIdField.Dispose ();
				itemIdField = null;
			}

			if (itemPathField != null) {
				itemPathField.Dispose ();
				itemPathField = null;
			}

			if (itemQueryField != null) {
				itemQueryField.Dispose ();
				itemQueryField = null;
			}

			if (deleteByIdButton != null) {
				deleteByIdButton.Dispose ();
				deleteByIdButton = null;
			}

			if (deleteByPathButton != null) {
				deleteByPathButton.Dispose ();
				deleteByPathButton = null;
			}

			if (deleteByQueryButton != null) {
				deleteByQueryButton.Dispose ();
				deleteByQueryButton = null;
			}
		}
	}
}
