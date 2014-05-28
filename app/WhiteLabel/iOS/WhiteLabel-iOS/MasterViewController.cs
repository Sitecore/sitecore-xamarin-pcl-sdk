using System;
using System.Drawing;
using System.Collections.Generic;
using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhiteLabeliOS
{
	public partial class MasterViewController : UITableViewController
	{
		DataSource dataSource;
		InstanceSettings settings;
		List<object> features = new List<object> ();

		public MasterViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("Master", "Master");
		}

		partial void settingsButtonTouched (MonoTouch.UIKit.UIBarButtonItem sender)
		{
			this.showSeetingsView();
		}

		private void showSeetingsView()
		{
			UINavigationController navController = this.NavigationController;

			UIStoryboard myStoryboard = this.Storyboard as UIStoryboard;
			SettingsViewController settingsViewController = myStoryboard.InstantiateViewController ("configurationViewController") as SettingsViewController;
			settingsViewController.instanceSettings = this.settings;

			navController.PushViewController (settingsViewController, true);
		}

		public override void ViewDidAppear(bool animated)
		{
			base.ViewDidAppear (animated);
			System.Console.WriteLine ("Current settings:\nURL: " + this.settings.InstanceUrl
				+ "\nLogin:    " + this.settings.InstanceLogin
				+ "\nPassword: " + this.settings.InstancePassword
				+ "\nSite:     " + this.settings.InstanceSite
				+ "\nDtaBase:  " + this.settings.InstanceDataBase);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.settings = new InstanceSettings();

			this.InitFeaturesList ();

			TableView.Source = dataSource = new DataSource (this);
			this.TableView.ReloadData();
		}

		private void InitFeaturesList()
		{
			this.features.Insert (0, "getItemByPath");
			this.features.Insert (0, "getItemById");
			this.features.Insert (0, "deleteItemById");
			this.features.Insert (0, "createEditItem");
			this.features.Insert (0, "uploadImageVC");

		}

		class DataSource : UITableViewSource
		{
			static readonly NSString CellIdentifier = new NSString ("Cell");
			readonly MasterViewController controller;

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
			// Customize the appearance of table view cells.
			public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
			{
				var cell = (UITableViewCell)tableView.DequeueReusableCell (CellIdentifier, indexPath);
				string featureKey = controller.features [indexPath.Row].ToString ();
				string featureTitle = NSBundle.MainBundle.LocalizedString (featureKey, null);
				cell.TextLabel.Text = featureTitle;

				return cell;
			}

			public override void RowSelected (UITableView tableView, NSIndexPath indexPath)
			{
				UINavigationController navController = controller.NavigationController;
				string featureKey = controller.features [indexPath.Row].ToString ();

				UIStoryboard myStoryboard = controller.Storyboard as UIStoryboard;
				BaseTaskViewController detailViewController = myStoryboard.InstantiateViewController (featureKey) as BaseTaskViewController;

				detailViewController.instanceSettings = controller.settings;

				navController.PushViewController (detailViewController, true);
			}
		}
			
	}
}

