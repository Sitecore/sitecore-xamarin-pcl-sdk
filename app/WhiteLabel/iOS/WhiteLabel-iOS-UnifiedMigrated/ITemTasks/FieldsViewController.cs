namespace WhiteLabeliOS
{
  using System;
  using System.Drawing;

  using Foundation;
  using UIKit;
  using WhiteLabeliOS.FieldsTableView;

  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Fields;

  public partial class FieldsViewController : BaseTaskTableViewController
  {
    protected FieldsDataSource fieldsDataSource;
    protected FieldCellSelectionHandler fieldsTableDelegate;


    public FieldsViewController(IntPtr handle) : base (handle)
    {
    }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      this.TableView = this.FieldsTableView;

    }

    public void ShowFieldsForItem(ISitecoreItem item)
    {
      BeginInvokeOnMainThread(delegate
      {
        this.Title = item.DisplayName;

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

    protected override void CleanupTableViewBindingsSync()
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
  }
}

