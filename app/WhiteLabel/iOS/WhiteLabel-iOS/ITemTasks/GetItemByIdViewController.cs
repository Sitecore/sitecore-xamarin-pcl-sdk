using System;
using System.Drawing;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.Items;

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

		partial void getItem (MonoTouch.UIKit.UIButton sender)
		{
			if (String.IsNullOrEmpty(itemIdTextField.Text))
			{
				AlertHelper.ShowAlertWithOkOption("Error", "Please type item Id");
			}
			else
			{
				this.sendRequest();
			}
		}

		partial void getChildren (MonoTouch.Foundation.NSObject sender)
		{
			AlertHelper.ShowAlertWithOkOption("Alert", "Not implemented yet");
		}

		private async void sendRequest ()
		{
			try
			{
				ScApiSession session = this.instanceSettings.GetSession();
				string itemId = itemIdTextField.Text;
				this.ShowLoader ();

				ScItemsResponse response = await session.ReadItemByIdAsync(itemId);

				this.HideLoader ();
				if (response.ResultCount > 0)
				{
					ScItem item = response.Items [0];
					AlertHelper.ShowAlertWithOkOption("Item received", "item title is \"" + item.DisplayName + "\"");
				}
				else
				{
					AlertHelper.ShowAlertWithOkOption("Message", "Item is not exist");
				}
			}
			catch(Exception e) 
			{
				AlertHelper.ShowAlertWithOkOption("Erorr", e.Message);
			}
		}
	}
}

