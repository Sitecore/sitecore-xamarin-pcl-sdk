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
	[Register ("GetItemByQueryViewController")]
	partial class GetItemByQueryViewController
	{
		[Outlet]
		UIButton getItemButton { get; set; }

		[Outlet]
		UITableView ItemsTableView { get; set; }

		[Outlet]
		UILabel nameLabel { get; set; }

		[Outlet]
		UITextField queryTextField { get; set; }

		[Action ("OnGetItemButtonTouched:")]
		partial void OnGetItemButtonTouched (NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{

			if (ItemsTableView != null) {
				ItemsTableView.Dispose ();
				ItemsTableView = null;
			}

			if (getItemButton != null) {
				getItemButton.Dispose ();
				getItemButton = null;
			}

			if (nameLabel != null) {
				nameLabel.Dispose ();
				nameLabel = null;
			}

			if (queryTextField != null) {
				queryTextField.Dispose ();
				queryTextField = null;
			}
		}
	}
}
