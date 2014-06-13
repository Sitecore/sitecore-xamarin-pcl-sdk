﻿using System.Linq;


namespace WhiteLabeliOS
{
    using System;
    using System.Drawing;

    using MonoTouch.Foundation;
    using MonoTouch.UIKit;

    using Sitecore.MobileSDK;
    using Sitecore.MobileSDK.Items;
    using Sitecore.MobileSDK.Items.Fields;
    using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

    using WhiteLabeliOS.FieldsTableView;



	public partial class GetItemByPathViewController : BaseTaskViewController
	{

		public GetItemByPathViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString("getItemByPath", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

            this.ItemPathField.ShouldReturn = this.HideKeyboard;
		}

		partial void OnGetItemButtonTouched (MonoTouch.Foundation.NSObject sender)
		{
            if (String.IsNullOrEmpty(this.ItemPathField.Text))
			{
				AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type item path");
			}
			else
			{
                this.HideKeyboard(this.ItemPathField);
                this.HideKeyboard(this.fieldNameTextField);

				this.SendRequest();
			}
		}

		private async void SendRequest ()
		{
			try
			{
				ScApiSession session = this.instanceSettings.GetSession();

                var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.ItemPathField.Text)
                    .Payload(PayloadType.Full)
                    .Build();

				this.ShowLoader();

				ScItemsResponse response = await session.ReadItemAsync(request);

				
                if (response.Items.Any())
				{
                    ISitecoreItem item = response.Items [0];
                    this.ShowFieldsForItem(item);

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
                CleanupTableViewBindings();

				AlertHelper.ShowLocalizedAlertWithOkOption("Erorr", e.Message);
			}
            finally
            {
                BeginInvokeOnMainThread(delegate
                {
                    this.FieldsTableView.ReloadData();
                    this.HideLoader();
                });
            }
		}

        void CleanupTableViewBindings()
        {
            BeginInvokeOnMainThread(delegate
            {
                this.FieldsTableView.DataSource = null;
                this.FieldsTableView.Delegate = null;
                this.fieldsDataSource.Dispose();
                this.fieldsDataSource = null;
                this.fieldsTableDelegate = null;
            });
        }

        private void ShowFieldsForItem( ISitecoreItem item )
        {
            BeginInvokeOnMainThread(delegate
            {
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

