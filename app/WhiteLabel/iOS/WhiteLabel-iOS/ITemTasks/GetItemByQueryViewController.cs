
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Sitecore.MobileSDK;
using Sitecore.MobileSDK.Items;

namespace WhiteLabeliOS
{
	public partial class GetItemByQueryViewController : BaseTaskViewController
	{
		public GetItemByQueryViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("getItemByQuery", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			queryTextField.Text = "/Sitecore/Content/Home/*";


			string getChildrenButtonTitle = NSBundle.MainBundle.LocalizedString ("Get Item", null);
			getItemButton.SetTitle (getChildrenButtonTitle, UIControlState.Normal);

			nameLabel.Text = NSBundle.MainBundle.LocalizedString ("Type query", null);
		}

		partial void OnGetItemButtonTouched (MonoTouch.Foundation.NSObject sender)
		{
			if (String.IsNullOrEmpty(queryTextField.Text))
			{
				AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type query");
			}
			else
			{
                this.HideKeyboard(this.queryTextField);
				this.SendRequest();
			}
		}

		private async void SendRequest ()
		{
			try
			{
				ScApiSession session = this.instanceSettings.GetSession();

				ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();

				var request = builder.RequestWithSitecoreQuery(queryTextField.Text)
					.Build();

				this.ShowLoader();

				ScItemsResponse response = await session.ReadItemByQueryAsync(request);

				this.HideLoader();
				if (response.ResultCount > 0)
				{
					string message = NSBundle.MainBundle.LocalizedString ("items count is", null);
					AlertHelper.ShowLocalizedAlertWithOkOption("Item received", message + " \"" + response.Items.Count.ToString() + "\"");
				}
				else
				{
					AlertHelper.ShowLocalizedAlertWithOkOption("Message", "Item is not exist");
				}
			}
			catch(Exception e) 
			{
				this.HideLoader();
				AlertHelper.ShowLocalizedAlertWithOkOption("Erorr", e.Message);
			}
		}
	}
}

