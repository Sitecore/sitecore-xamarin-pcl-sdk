


namespace WhiteLabeliOS
{
    using System;
    using System.Drawing;
	using System.Linq;

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

			fieldNameTextField.Placeholder = NSBundle.MainBundle.LocalizedString ("Type field name", null);
			ItemPathField.Placeholder = NSBundle.MainBundle.LocalizedString ("Type item Path", null);
			getItemButton.SetTitle (NSBundle.MainBundle.LocalizedString ("Get Item", null), UIControlState.Normal);
		}

		partial void OnGetItemButtonTouched (MonoTouch.Foundation.NSObject sender)
		{
            if (String.IsNullOrEmpty(this.ItemPathField.Text))
			{
				AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type item path");
			}
			else
			{
				this.SendRequest();
			}
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

		private async void SendRequest ()
		{
			try
			{
				ScApiSession session = this.instanceSettings.GetSession();

				ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();

                var request = builder.RequestWithPath(this.ItemPathField.Text)
					.Payload(this.currentPayloadType)
					.AddSingleField(this.fieldNameTextField.Text)
					.Build();

				this.ShowLoader();

				ScItemsResponse response = await session.ReadItemByPathAsync(request);

				
                if (response.Items.Any())
				{
					ScItem item = response.Items [0];
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
				this.CleanupTableViewBindingsAsync();

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

		void CleanupTableViewBindingsAsync()
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
			}

            if (this.fieldsTableDelegate != null)
            {
                this.fieldsTableDelegate.Dispose ();
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

