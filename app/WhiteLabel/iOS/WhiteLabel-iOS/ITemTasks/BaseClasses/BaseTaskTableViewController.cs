
namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;
  using System.Collections.Generic;

  using MonoTouch.Foundation;
  using MonoTouch.UIKit;

  using WhiteLabeliOS.FieldsTableView;

  using Sitecore.MobileSDK.API.Items;

	public partial class BaseTaskTableViewController : BaseTaskViewController
	{
		public BaseTaskTableViewController (IntPtr handle) : base (handle)
		{
		
		}

		protected virtual void CleanupTableViewBindings()
		{
			BeginInvokeOnMainThread(delegate
			{
				this.CleanupTableViewBindingsSync();
			});
		}

    protected virtual void CleanupTableViewBindingsSync()
		{
			this.TableView.DataSource = null;
			this.TableView.Delegate = null;

      if (this.itemsDataSource != null)
      {
        this.itemsDataSource.Dispose ();
        this.itemsDataSource = null;
      }

      if (this.itemsTableDelegate != null)
			{
        this.itemsTableDelegate.Dispose ();
        this.itemsTableDelegate = null;
			}
		}

    protected void ShowItemsList( IEnumerable<ISitecoreItem> items )
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
          this.selectedItem = item;
          this.PerformSegue("showFieldsViewController", this);
          //AlertHelper.ShowLocalizedAlertWithOkOption("Message", item.DisplayName);
        };
        tableDelegate.OnItemCellSelectedDelegate = onItemSelected;

        this.TableView.DataSource = dataSource;
        this.TableView.Delegate = tableDelegate;
        this.TableView.ReloadData();
      });
    }

    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      base.PrepareForSegue(segue, sender);

      if ("showFieldsViewController" == segue.Identifier)
      {
        FieldsViewController viewController = segue.DestinationViewController as FieldsViewController;
        viewController.ShowFieldsForItem (this.selectedItem);
      }
    
    }

		protected MonoTouch.UIKit.UITableView TableView;
    protected ISitecoreItem selectedItem;
    protected ItemsDataSource itemsDataSource;
    protected ItemCellSelectionHandler itemsTableDelegate;
	}
}

