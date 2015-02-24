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
	[Register ("DeleteITemByIdViewController")]
	partial class DeleteITemByIdViewController
	{
		[Outlet]
		UIKit.UIButton deleteByIdButton { get; set; }

		[Outlet]
		UIKit.UIButton deleteByPathButton { get; set; }

		[Outlet]
		UIKit.UIButton deleteByQueryButton { get; set; }

		[Outlet]
		UIKit.UITextField itemIdField { get; set; }

		[Outlet]
		UIKit.UITextField itemPathField { get; set; }

		[Outlet]
		UIKit.UITextField itemQueryField { get; set; }

		[Action ("OnDeleteItemByIdButtonTouched:")]
		partial void OnDeleteItemByIdButtonTouched (UIKit.UIButton sender);

		[Action ("OnDeleteItemByPathButtonTouched:")]
		partial void OnDeleteItemByPathButtonTouched (UIKit.UIButton sender);

		[Action ("OnDeleteItemByqueryButtonTouched:")]
		partial void OnDeleteItemByqueryButtonTouched (UIKit.UIButton sender);
		
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
