using System.Linq;


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



	public partial class GetItemByPathViewController : BaseTaskTableViewController
	{

		public GetItemByPathViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString("getItemByPath", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			this.TableView = this.FieldsTableView;

			this.ItemPathField.Text = "/sitecore/content/Home";

            this.ItemPathField.ShouldReturn = this.HideKeyboard;

			this.ItemPathField.Placeholder = NSBundle.MainBundle.LocalizedString ("Type item Path", null);
			this.fieldNameTextField.Placeholder = NSBundle.MainBundle.LocalizedString ("Type field name", null);;

			string getItemButtonTitle = NSBundle.MainBundle.LocalizedString ("Get Item", null);
			getItemButton.SetTitle (getItemButtonTitle, UIControlState.Normal);

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

                var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath(this.ItemPathField.Text)
					.Payload(this.currentPayloadType)
					.AddFields(this.fieldNameTextField.Text)
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
				this.CleanupTableViewBindings();
				AlertHelper.ShowLocalizedAlertWithOkOption("Error", e.Message);
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
			
	}
}

