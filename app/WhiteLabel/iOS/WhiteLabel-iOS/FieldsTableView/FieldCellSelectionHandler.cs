
namespace WhiteLabeliOS.FieldsTableView
{
    using System;

    using MonoTouch.UIKit;
    using MonoTouch.Foundation;

    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.Items.Fields;


// https://github.com/sami1971/SimplyMobile/wiki/Cross-platform-data-source
// http://components.xamarin.com/view/gmgsoftware

    public class FieldCellSelectionHandler : UITableViewDelegate
    {
        public delegate void TableViewDidSelectFieldAtIndexPath(UITableView tableView, IField itemField, NSIndexPath indexPath);

        protected override void Dispose (bool disposing)
        {
            InvokeOnMainThread(delegate
            {
                this.handler = null;
                this.sitecoreItem = null;

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
                throw new ArgumentNullException("FieldsDataSource.TableView cannot be null");
            }
            else if (null == this.sitecoreItem)
            {
                throw new ArgumentNullException("FieldsDataSource.SitecoreItem cannot be null");
            }
            else if (null == this.OnFieldCellSelectedDelegate)
            {
                throw new ArgumentNullException("FieldsDataSource.SitecoreItem cannot be null");
            }
        }

        public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
        {
            this.ValidateFields();

            IField selectedField = this.sitecoreItem.Fields[indexPath.Row];
            this.OnFieldCellSelectedDelegate(tableView, selectedField, indexPath);
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
                    throw new InvalidOperationException("FieldCellSelectionHandler.TableView cannot be assigned twice");
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
                    throw new InvalidOperationException("FieldCellSelectionHandler.Item cannot be assigned twice");
                }

                this.sitecoreItem = value;
            }
        }

        public TableViewDidSelectFieldAtIndexPath OnFieldCellSelectedDelegate
        {
            get
            {
                return this.handler;
            }
            set
            {
                if (null != this.handler)
                {
                    throw new InvalidOperationException("FieldCellSelectionHandler.OnFieldCellSelectedDelegate cannot be assigned twice");
                }

                this.handler = value;
            }
        }
        #endregion Properties


        #region Instance Variables
        private ISitecoreItem sitecoreItem;
        private UITableView myTable;
        private TableViewDidSelectFieldAtIndexPath handler;
        #endregion Instance Variables
    }
}

