
namespace WhiteLabeliOS.FieldsTableView
{
  using System;
  using System.Linq;

  #if __UNIFIED__
  using UIKit;
  using Foundation;
  #else
  using MonoTouch.UIKit;
  using MonoTouch.Foundation;
  #endif

  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Fields;

  public class FieldsDataSource : UITableViewDataSource
  {
    protected override void Dispose (bool disposing)
    {
      InvokeOnMainThread(delegate
      {
        this.sitecoreItem = null;

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
        throw new ArgumentNullException("FieldsDataSource.TableView cannot be null");
      }
      else if (null == this.sitecoreItem)
      {
        throw new ArgumentNullException("FieldsDataSource.SitecoreItem cannot be null");
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
          throw new InvalidOperationException("FieldsDataSource.TableView cannot be assigned twice");
        }

        this.myTable = value;
      }

    }

    public ISitecoreItem SitecoreItem
    { 
      get
      {
        return this.sitecoreItem;
      }
      set
      {
        if (null != this.sitecoreItem)
        {
          throw new InvalidOperationException("FieldsDataSource.Item cannot be assigned twice");
        }

        this.sitecoreItem = value;
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
      if (null == this.sitecoreItem)
      {
        //@adk : workaround for unexpected UIKit behaviour
        return 0;
      }

      this.ValidateFields();
      return this.sitecoreItem.FieldsCount;
    }

    public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      this.ValidateFields();

      const string FIELD_CELL_ID = "SitecoreFieldCell";

      UITableViewCell result = tableView.DequeueReusableCell(FIELD_CELL_ID);
      if (null == result)
      {
        result = new UITableViewCell(UITableViewCellStyle.Default, FIELD_CELL_ID);
      }

      IField currentField = this.sitecoreItem.Fields.ElementAt(indexPath.Row);
      result.TextLabel.Text = currentField.Name;

      return result;
    }

    public override string TitleForHeader (UITableView tableView, int section)
    {
      return this.sitecoreItem.DisplayName;
    }
    #endregion UITableViewDataSource

    #region Instance Variables
    private ISitecoreItem sitecoreItem;
    private UITableView myTable;
    #endregion Instance Variables
  }
}

