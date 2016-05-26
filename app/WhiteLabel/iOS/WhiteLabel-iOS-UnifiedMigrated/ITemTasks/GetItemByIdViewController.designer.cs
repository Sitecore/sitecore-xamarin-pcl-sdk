// WARNING
//
// This file has been generated automatically by Xamarin Studio Business to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace WhiteLabeliOS
{
	[Register ("GetItemByIdViewController")]
	partial class GetItemByIdViewController
	{
		[Outlet]
		UIKit.UIButton childrenScopeButton { get; set; }

		[Outlet]
		UIKit.UITextField fieldNameTextField { get; set; }

		[Outlet]
		UIKit.UITableView FieldsTableView { get; set; }

		[Outlet]
		UIKit.UIButton getChildrenButton { get; set; }

		[Outlet]
		UIKit.UIButton getItemButton { get; set; }

		[Outlet]
		UIKit.UITextField itemIdTextField { get; set; }

		[Outlet]
		UIKit.UIButton parentScopeButton { get; set; }

		[Outlet]
		UIKit.UISegmentedControl PayloadSelectionView { get; set; }

		[Outlet]
		UIKit.UIButton selfScopeButton { get; set; }

		[Action ("OnButtonChangeState:")]
		partial void OnButtonChangeState (UIKit.UIButton sender);

		[Action ("OnChildrenButtonTouched:")]
		partial void OnChildrenButtonTouched (Foundation.NSObject sender);

		[Action ("OnGetItemButtonTouched:")]
		partial void OnGetItemButtonTouched (Foundation.NSObject sender);

		[Action ("OnPayloadValueChanged:")]
		partial void OnPayloadValueChanged (UIKit.UISegmentedControl sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (parentScopeButton != null) {
				parentScopeButton.Dispose ();
				parentScopeButton = null;
			}

			if (selfScopeButton != null) {
				selfScopeButton.Dispose ();
				selfScopeButton = null;
			}

			if (childrenScopeButton != null) {
				childrenScopeButton.Dispose ();
				childrenScopeButton = null;
			}

			if (fieldNameTextField != null) {
				fieldNameTextField.Dispose ();
				fieldNameTextField = null;
			}

			if (FieldsTableView != null) {
				FieldsTableView.Dispose ();
				FieldsTableView = null;
			}

			if (getChildrenButton != null) {
				getChildrenButton.Dispose ();
				getChildrenButton = null;
			}

			if (getItemButton != null) {
				getItemButton.Dispose ();
				getItemButton = null;
			}

			if (itemIdTextField != null) {
				itemIdTextField.Dispose ();
				itemIdTextField = null;
			}

			if (PayloadSelectionView != null) {
				PayloadSelectionView.Dispose ();
				PayloadSelectionView = null;
			}
		}
	}
}
