
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WhiteLabeliOS.FieldsTableView;
using Sitecore.MobileSDK.Items.Fields;
using Sitecore.MobileSDK.Items;

namespace WhiteLabeliOS
{
	public partial class BaseTaskTableViewController : BaseTaskViewController
	{
		public BaseTaskTableViewController (IntPtr handle) : base (handle)
		{
		
		}

		protected void CleanupTableViewBindings()
		{
			BeginInvokeOnMainThread(delegate
			{
				this.CleanupTableViewBindingsSync();
			});
		}

		protected void CleanupTableViewBindingsSync()
		{
			this.TableView.DataSource = null;
			this.TableView.Delegate = null;

			if (this.fieldsDataSource != null)
			{
				this.fieldsDataSource.Dispose ();
				this.fieldsDataSource = null;
			}

			if (this.fieldsTableDelegate != null)
			{
				this.fieldsTableDelegate.Dispose ();
				this.fieldsTableDelegate = null;
			}
		}

		protected void ShowFieldsForItem( ISitecoreItem item )
		{
			BeginInvokeOnMainThread(delegate
			{
				this.CleanupTableViewBindingsSync();

				this.fieldsDataSource = new FieldsDataSource();
				this.fieldsTableDelegate = new FieldCellSelectionHandler();


				FieldsDataSource dataSource = this.fieldsDataSource;
				dataSource.SitecoreItem = item;
				dataSource.TableView = this.TableView;


				FieldCellSelectionHandler tableDelegate = this.fieldsTableDelegate;
				tableDelegate.TableView = this.TableView;
				tableDelegate.SitecoreItem = item;

				FieldCellSelectionHandler.TableViewDidSelectFieldAtIndexPath onFieldSelected = 
					delegate (UITableView tableView, IField itemField, NSIndexPath indexPath)
				{
					AlertHelper.ShowLocalizedAlertWithOkOption("Field Raw Value", itemField.RawValue);
				};
				tableDelegate.OnFieldCellSelectedDelegate = onFieldSelected;



				this.TableView.DataSource = dataSource;
				this.TableView.Delegate = tableDelegate;
				this.TableView.ReloadData();
			});
		}

		protected MonoTouch.UIKit.UITableView TableView;

		protected FieldsDataSource fieldsDataSource;
		protected FieldCellSelectionHandler fieldsTableDelegate;
	}
}

