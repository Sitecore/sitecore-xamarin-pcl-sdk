


namespace WhiteLabeliOS
{
    using System;

    using MonoTouch.UIKit;
    using MonoTouch.Foundation;

    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.Items.Fields;

    public class FieldsDataSource : UITableViewDataSource
    {
        protected override void Dispose (bool disposing)
        {
            this.sitecoreItem = null;
            this.myTable.DataSource = null;

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

        public ScItem SitecoreItem
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
            return this.sitecoreItem.mFields.Count;
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

            IField currentField = this.sitecoreItem.mFields[indexPath.Row];
            result.TextLabel.Text = currentField.Name;

            return result;
        }
        #endregion UITableViewDataSource

        #region Instance Variables
        private ScItem sitecoreItem;
        private UITableView myTable;
        #endregion Instance Variables
    }
}

