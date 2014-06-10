using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.Items;
using Sitecore.MobileSDK.UrlBuilder;

namespace WhiteLabeliOS
{
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

		private async void SendRequest()
		{
			try
			{
				ScApiSession session = this.instanceSettings.GetSession();

				ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();

				var request = builder.RequestWithId(itemIdTextField.Text)
					.Build();
					
				this.ShowLoader();

				ScItemsResponse response = await session.ReadItemByIdAsync(request);

				this.HideLoader();
				if (response.ResultCount > 0)
				{
					ScItem item = response.Items [0];
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
				this.HideLoader();
				AlertHelper.ShowLocalizedAlertWithOkOption("Erorr", e.Message);
			}
		}
	}
}

