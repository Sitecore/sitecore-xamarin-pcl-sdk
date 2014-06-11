using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.Items;

namespace WhiteLabeliOS
{
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
				this.SendRequest();
			}
		}

		private async void SendRequest ()
		{
			try
			{
				ScApiSession session = this.instanceSettings.GetSession();

				ItemWebApiRequestBuilder builder = new ItemWebApiRequestBuilder();

                var request = builder.RequestWithPath(this.ItemPathField.Text)
					.Build();

				this.ShowLoader();

				ScItemsResponse response = await session.ReadItemByPathAsync(request);

				
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
				AlertHelper.ShowLocalizedAlertWithOkOption("Erorr", e.Message);
			}
            finally
            {
                this.HideLoader();
            }
		}
	}
}

