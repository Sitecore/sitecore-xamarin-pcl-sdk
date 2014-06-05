
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
			
		}

		partial void getItem (MonoTouch.Foundation.NSObject sender)
		{
			if (String.IsNullOrEmpty(queryTextField.Text))
			{
				AlertHelper.ShowAlertWithOkOption("Error", "Please type query");
			}
			else
			{
				this.sendRequest();
			}
		}

		private async void sendRequest ()
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
					AlertHelper.ShowAlertWithOkOption("Item received", "items count is \"" + response.Items.Count.ToString() + "\"");
				}
				else
				{
					AlertHelper.ShowAlertWithOkOption("Message", "Item is not exist");
				}
			}
			catch(Exception e) 
			{
				this.HideLoader();
				AlertHelper.ShowAlertWithOkOption("Erorr", e.Message);
			}
		}
	}
}

