
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using WhiteLabeliOS.FieldsTableView;
using Sitecore.MobileSDK.Items.Fields;
using Sitecore.MobileSDK.Items;
using System.Collections.Generic;

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

      if (this.itemsTableDelegate != null)
			{
        this.itemsTableDelegate.Dispose ();
        this.itemsTableDelegate = null;
			}
		}

    protected void ShowItemsList( List<ISitecoreItem> items )
    {
      BeginInvokeOnMainThread(delegate
      {
        this.CleanupTableViewBindingsSync();

        this.itemsDataSource = new ItemsDataSource();
        this.itemsTableDelegate = new ItemCellSelectionHandler();


        ItemsDataSource dataSource = this.itemsDataSource;
        dataSource.SitecoreItems = items;
        dataSource.TableView = this.TableView;


        ItemCellSelectionHandler tableDelegate = this.itemsTableDelegate;
        tableDelegate.TableView = this.TableView;
        tableDelegate.SitecoreItems = items;

        ItemCellSelectionHandler.TableViewDidSelectItemAtIndexPath onItemSelected = 
          delegate (UITableView tableView, ISitecoreItem item, NSIndexPath indexPath)
        {
         //TODO: @igk show fields list here!!!
        };
        tableDelegate.OnItemCellSelectedDelegate = onItemSelected;

        this.TableView.DataSource = dataSource;
        this.TableView.Delegate = tableDelegate;
        this.TableView.ReloadData();
      });
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
    protected ItemsDataSource itemsDataSource;

    protected FieldCellSelectionHandler fieldsTableDelegate;
    protected ItemCellSelectionHandler itemsTableDelegate;
	}
}

