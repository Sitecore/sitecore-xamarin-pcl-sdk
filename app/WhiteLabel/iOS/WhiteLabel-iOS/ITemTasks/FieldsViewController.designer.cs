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
	[Register ("FieldsViewController")]
	partial class FieldsViewController
	{
		[Outlet]
		MonoTouch.UIKit.UITableView FieldsTableView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (FieldsTableView != null) {
				FieldsTableView.Dispose ();
				FieldsTableView = null;
			}
		}
	}
}
