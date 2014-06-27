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


	public partial class GetItemByIdViewController : BaseTaskTableViewController
	{
		public GetItemByIdViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("getItemById", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.TableView = this.FieldsTableView;

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
                this.HideKeyboard(this.itemIdTextField);
                this.HideKeyboard(this.fieldNameTextField);
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

                var request = ItemWebApiRequestBuilder.ReadItemsRequestWithId(itemIdTextField.Text)
					.Payload(this.currentPayloadType)
                    .AddFields(this.fieldNameTextField.Text)
				    .Build();

				this.ShowLoader();

				ScItemsResponse response = await session.ReadItemAsync(request);
                if (response.Items.Any())
				{
                    ISitecoreItem item = response.Items[0];
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

        

       
	}
}

