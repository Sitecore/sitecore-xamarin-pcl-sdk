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
	[Register ("GetItemByPathViewController")]
	partial class GetItemByPathViewController
	{
		[Outlet]
		UIButton childrenScopeButton { get; set; }

		[Outlet]
		UITextField fieldNameTextField { get; set; }

		[Outlet]
		UITableView FieldsTableView { get; set; }

		[Outlet]
		UIButton getItemButton { get; set; }

		[Outlet]
		UITextField ItemPathField { get; set; }

		[Outlet]
		UISegmentedControl PayloadSelectionView { get; set; }

    [Outlet]
		UIButton parentScopeButton { get; set; }

		[Outlet]
		UIButton selfScopeButton { get; set; }

		[Action ("OnButtonChangeState:")]
		partial void OnButtonChangeState (UIButton sender);

		[Action ("OnGetItemButtonTouched:")]
		partial void OnGetItemButtonTouched (NSObject sender);

		[Action ("OnPayloadValueChanged:")]
		partial void OnPayloadValueChanged (UISegmentedControl sender);
		
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

			if (getItemButton != null) {
				getItemButton.Dispose ();
				getItemButton = null;
			}

			if (ItemPathField != null) {
				ItemPathField.Dispose ();
				ItemPathField = null;
			}

			if (PayloadSelectionView != null) {
				PayloadSelectionView.Dispose ();
				PayloadSelectionView = null;
			}
		}
	}
}
