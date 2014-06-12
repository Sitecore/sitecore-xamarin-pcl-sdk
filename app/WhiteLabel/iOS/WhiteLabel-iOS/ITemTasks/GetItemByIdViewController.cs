﻿using Sitecore.MobileSDK.Items.Fields;


namespace WhiteLabeliOS
{
	using System;
    using System.Linq;

    using MonoTouch.UIKit;
	using MonoTouch.Foundation;

	using Sitecore.MobileSDK;
	using Sitecore.MobileSDK.Items;
	using Sitecore.MobileSDK.UrlBuilder;
	using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    using WhiteLabeliOS.FieldsTableView;


	public partial class GetItemByIdViewController : BaseTaskViewController
	{
		public GetItemByIdViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("getItemById", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			this.itemIdTextField.ShouldReturn = this.HideKeyboard;

			fieldNameTextField.Placeholder = NSBundle.MainBundle.LocalizedString ("Type field name", null);
			itemIdTextField.Placeholder = NSBundle.MainBundle.LocalizedString ("Type item ID", null);

			string getChildrenButtonTitle = NSBundle.MainBundle.LocalizedString ("Get Item children", null);
			getChildrenButton.SetTitle (getChildrenButtonTitle, UIControlState.Normal);
			string getItemButtonTitle = NSBundle.MainBundle.LocalizedString ("Get Item", null);
			getItemButton.SetTitle (getItemButtonTitle, UIControlState.Normal);
		}

		partial void OnGetItemButtonTouched (MonoTouch.Foundation.NSObject sender)
		{
			if (String.IsNullOrEmpty(itemIdTextField.Text))
			{
				AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type item Id");
			}
			else
			{
				this.SendRequest();
			}
		}

		partial void OnGetItemCheldrenButtonTouched (MonoTouch.Foundation.NSObject sender)
		{
			AlertHelper.ShowLocalizedNotImlementedAlert();
		}

		partial void OnPayloadValueChanged (MonoTouch.UIKit.UISegmentedControl sender)
		{
			switch (sender.SelectedSegment)
			{
			case 0:
				this.currentPayloadType = PayloadType.Full;
				break;
			case 1:
				this.currentPayloadType = PayloadType.Content;
				break;
			case 2:
				this.currentPayloadType = PayloadType.Min;
				break;
			}
		}

		private async void SendRequest()
		{
			try
			{
				ScApiSession session = this.instanceSettings.GetSession();

				ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();

				var request = builder.RequestWithId(itemIdTextField.Text)
					.Payload(this.currentPayloadType)
					.AddSingleField(this.fieldNameTextField.Text)
				    .Build();

				this.ShowLoader();

				ScItemsResponse response = await session.ReadItemByIdAsync(request);
                if (response.Items.Any())
				{
					ScItem item = response.Items[0];
                    this.ShowFieldsForItem( item );

					string message = NSBundle.MainBundle.LocalizedString("item title is", null);
					AlertHelper.ShowLocalizedAlertWithOkOption("Item received", message + " \"" + item.DisplayName + "\"");
				}
				else
				{
					AlertHelper.ShowLocalizedAlertWithOkOption("Message", "Item is not exist");
				}
			}
			catch(Exception e) 
			{
                this.CleanupTableViewBindings();
				AlertHelper.ShowLocalizedAlertWithOkOption("Erorr", e.Message);
			}
            finally
            {
                BeginInvokeOnMainThread(delegate
                {
                    this.HideLoader();
                    this.FieldsTableView.ReloadData();
                });
            }
		}

        void CleanupTableViewBindings()
        {
            BeginInvokeOnMainThread(delegate
            {
				this.CleanupTableViewBindingsSync();
            });
        }

		void CleanupTableViewBindingsSync()
		{
			this.FieldsTableView.DataSource = null;
			this.FieldsTableView.Delegate = null;

			if (this.fieldsDataSource != null)
			{
				this.fieldsDataSource.Dispose ();
				this.fieldsDataSource = null;
				this.fieldsTableDelegate = null;
			}
		}

        private void ShowFieldsForItem( ScItem item )
        {
            BeginInvokeOnMainThread(delegate
            {
				this.CleanupTableViewBindingsSync();

                this.fieldsDataSource = new FieldsDataSource();
                this.fieldsTableDelegate = new FieldCellSelectionHandler();


                FieldsDataSource dataSource = this.fieldsDataSource;
                dataSource.SitecoreItem = item;
                dataSource.TableView = this.FieldsTableView;


                FieldCellSelectionHandler tableDelegate = this.fieldsTableDelegate;
                tableDelegate.TableView = this.FieldsTableView;
                tableDelegate.SitecoreItem = item;

                FieldCellSelectionHandler.TableViewDidSelectFieldAtIndexPath onFieldSelected = 
                    delegate (UITableView tableView, IField itemField, NSIndexPath indexPath)
                    {
                        AlertHelper.ShowLocalizedAlertWithOkOption("Field Raw Value", itemField.RawValue);
                    };
                tableDelegate.OnFieldCellSelectedDelegate = onFieldSelected;



                this.FieldsTableView.DataSource = dataSource;
                this.FieldsTableView.Delegate = tableDelegate;
                this.FieldsTableView.ReloadData();
            });
        }

        private FieldsDataSource fieldsDataSource;
        private FieldCellSelectionHandler fieldsTableDelegate;
	}
}

