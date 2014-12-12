
namespace WhiteLabeliOS.FieldsTableView
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using UIKit;
  using Foundation;

  using Sitecore.MobileSDK.API.Items;

  // https://github.com/sami1971/SimplyMobile/wiki/Cross-platform-data-source
  // http://components.xamarin.com/view/gmgsoftware

  public class ItemCellSelectionHandler : UITableViewDelegate
  {
    public delegate void TableViewDidSelectItemAtIndexPath(UITableView tableView, ISitecoreItem item, NSIndexPath indexPath);

    protected override void Dispose (bool disposing)
    {
      InvokeOnMainThread(delegate
      {
        this.handler = null;
        this.sitecoreItems = null;

        if (null != this.myTable)
        {
          this.myTable.Delegate = null;
        }
        this.myTable = null;
      });


      base.Dispose(disposing);
    }

    private void ValidateFields()
    {
      if (null == this.myTable)
      {
        throw new ArgumentNullException("ItemCellSelectionHandler.TableView cannot be null");
      }
      else if (null == this.sitecoreItems)
      {
        throw new ArgumentNullException("ItemCellSelectionHandler.SitecoreItems cannot be null");
      }
      else if (null == this.OnItemCellSelectedDelegate)
      {
        throw new ArgumentNullException("ItemCellSelectionHandler.OnItemCellSelectedDelegate cannot be null");
      }
    }

    public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
    {
      this.ValidateFields();

      ISitecoreItem selectedItem = this.sitecoreItems.ElementAt(indexPath.Row);
      this.OnItemCellSelectedDelegate(tableView, selectedItem, indexPath);
    }

    #region Properties
    public UITableView TableView
    {
      get
      {
        return this.myTable;
      }
      set
      {
        if (null != this.myTable)
        {
          throw new InvalidOperationException("ItemCellSelectionHandler.TableView cannot be assigned twice");
        }

        this.myTable = value;
      }

    }

    public IEnumerable<ISitecoreItem> SitecoreItems
    { 
      get
      {
        return this.sitecoreItems;
      }
      set
      {
        if (null != this.sitecoreItems)
        {
          throw new InvalidOperationException("ItemCellSelectionHandler.SitecoreItems cannot be assigned twice");
        }

        this.sitecoreItems = value;
      }
    }

    public TableViewDidSelectItemAtIndexPath OnItemCellSelectedDelegate
    {
      get
      {
        return this.handler;
      }
      set
      {
        if (null != this.handler)
        {
          throw new InvalidOperationException("ItemCellSelectionHandler.OnItemCellSelectedDelegate cannot be assigned twice");
        }

        this.handler = value;
      }
    }
    #endregion Properties


    #region Instance Variables
    private IEnumerable<ISitecoreItem> sitecoreItems;
    private UITableView myTable;
    private TableViewDidSelectItemAtIndexPath handler;
    #endregion Instance Variables
  }
}

