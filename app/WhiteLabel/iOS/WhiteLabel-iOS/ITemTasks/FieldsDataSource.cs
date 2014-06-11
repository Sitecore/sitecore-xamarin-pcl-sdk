


namespace WhiteLabeliOS
{
    using System;
    using MonoTouch.UIKit;
    using Sitecore.MobileSDK.Items;

    public class FieldsDataSource : UITableViewDataSource
    {
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

        public override int NumberOfSections(UITableView tableView)
        {
            return 1;
        }

        public override int RowsInSection(UITableView tableView, int section)
        {
            if (null == this.sitecoreItem)
            {
                return 0;
            }

            return this.sitecoreItem.
        }


        private ScItem sitecoreItem;
    }
}

