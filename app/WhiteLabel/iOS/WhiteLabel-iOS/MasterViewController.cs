using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhiteLabeliOS
{
	public partial class MasterViewController : UITableViewController
	{
        #region UIViewController
		public MasterViewController (IntPtr handle) : base (handle)
		{
            this.Title = NSBundle.MainBundle.LocalizedString("Master", "Master");
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear (animated);
			System.Console.WriteLine ("Current settings:\n"
				+ "\nURL:       " + this.settings.InstanceUrl
				+ "\nLogin:     " + this.settings.InstanceLogin
				+ "\nPassword:  " + this.settings.InstancePassword
				+ "\nSite:      " + this.settings.InstanceSite
				+ "\nDataBase:  " + this.settings.InstanceDataBase);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.settings = new InstanceSettings();
			this.InitFeaturesList ();

			this.dataSource = new DataSource (this);
            this.TableView.Source = this.dataSource;
			this.TableView.ReloadData();
		}
        #endregion UIViewController

        #region Navigation
        public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
        {
            base.PrepareForSegue(segue, sender);

            if (segue.Identifier == "configurationViewController")
            {
                var settingsViewController = segue.DestinationViewController as SettingsViewController;
                settingsViewController.instanceSettings = this.settings;
            }
            else
            {
                var targetController = segue.DestinationViewController as BaseTaskViewController;
                targetController.instanceSettings = this.settings;

            }
        }

        private void InitFeaturesList()
        {
            this.features.Insert(0, "getItemByPath");
            this.features.Insert(0, "getItemById");
            this.features.Insert(0, "deleteItemById");
            this.features.Insert(0, "createEditItem");
            this.features.Insert(0, "uploadImageVC");
            this.features.Insert(0, "getItemByQuery");

        }
        #endregion Navigation

        #region Instance Variables
        private DataSource dataSource;
        private InstanceSettings settings;
        private List<object> features = new List<object> ();
        #endregion Instance Variables

		class DataSource : UITableViewSource
		{
            #region UITableViewDataSource
			public DataSource (MasterViewController controller)
			{
				this.controller = controller;
			}

			public override int NumberOfSections (UITableView tableView)
			{
				return 1;
			}

			public override int RowsInSection (UITableView tableview, int section)
			{
				return controller.features.Count;
			}

			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
                var cell = tableView.DequeueReusableCell (CellIdentifier, indexPath);
				string featureKey = controller.features [indexPath.Row].ToString ();
				string featureTitle = NSBundle.MainBundle.LocalizedString (featureKey, null);
				cell.TextLabel.Text = featureTitle;

				return cell;
			}
            #endregion UITableViewDataSource


            #region UITableViewDelegate
			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				UINavigationController navController = controller.NavigationController;
				string featureKey = controller.features [indexPath.Row].ToString ();

                this.controller.PerformSegue(featureKey, this.controller);
			}
            #endregion UITableViewDelegate


            #region Instance Variables
            private static readonly NSString CellIdentifier = new NSString ("Cell");
            private readonly MasterViewController controller;
            #endregion Instance Variables
		}
	}
}

