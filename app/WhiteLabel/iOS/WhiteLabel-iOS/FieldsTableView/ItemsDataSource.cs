

namespace WhiteLabeliOS.FieldsTableView
{
  using System;
  using System.Linq;
  using System.Collections.Generic;
  using MonoTouch.UIKit;
  using MonoTouch.Foundation;

  using Sitecore.MobileSDK.API.Items;


  public class ItemsDataSource : UITableViewDataSource
  {
    protected override void Dispose (bool disposing)
    {
      InvokeOnMainThread(delegate
      {
        this.sitecoreItems = null;

        if (null != this.myTable)
        {
          this.myTable.DataSource = null;
        }
        this.myTable = null;
      });

      base.Dispose(disposing);
    }

    private void ValidateFields()
    {
      if (null == this.myTable)
      {
        throw new ArgumentNullException("ItemsDataSource.TableView cannot be null");
      }
      else if (null == this.sitecoreItems)
      {
        throw new ArgumentNullException("ItemsDataSource.SitecoreItems cannot be null");
      }
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
          throw new InvalidOperationException("ItemsDataSource.TableView cannot be assigned twice");
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
          throw new InvalidOperationException("ItemsDataSource.Item cannot be assigned twice");
        }

        this.sitecoreItems = value;
      }
    }
    #endregion Properties


    #region UITableViewDataSource
    public override int NumberOfSections(UITableView tableView)
    {
      return 1;
    }

    public override int RowsInSection(UITableView tableView, int section)
    {
      if (null == this.sitecoreItems)
      {
        //@adk : workaround for unexpected UIKit behaviour
        return 0;
      }

      this.ValidateFields();
      return this.sitecoreItems.Count();
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      this.ValidateFields();

      const string FIELD_CELL_ID = "SitecoreItemCell";

      UITableViewCell result = tableView.DequeueReusableCell(FIELD_CELL_ID);
      if (null == result)
      {
        result = new UITableViewCell(UITableViewCellStyle.Default, FIELD_CELL_ID);
      }

      ISitecoreItem currentItem = this.sitecoreItems.ElementAt(indexPath.Row);
      result.TextLabel.Text = currentItem.DisplayName;

      return result;
    }

    public override string TitleForHeader (UITableView tableView, int section)
    {
      return "Items List";
    }
    #endregion UITableViewDataSource

    #region Instance Variables
    private IEnumerable<ISitecoreItem> sitecoreItems;
    private UITableView myTable;
    #endregion Instance Variables
  }
}

