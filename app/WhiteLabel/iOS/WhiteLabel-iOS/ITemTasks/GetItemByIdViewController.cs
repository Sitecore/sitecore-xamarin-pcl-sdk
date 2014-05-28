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
			
			// Perform any additional setup after loading the view, typically from a nib.
		}

		partial void getItem (MonoTouch.UIKit.UIButton sender)
		{
			if (String.IsNullOrEmpty(itemIdTextField.Text))
			{
				AlertHelper.ShowErrorAlertWithOkOption("Error", "Please type item Id");
			}
			else
			{
				this.sendRequest();
			}
		}

		partial void getChildren (MonoTouch.Foundation.NSObject sender)
		{
			AlertHelper.ShowErrorAlertWithOkOption("Alert", "Not implemented yet");
		}

		private async void sendRequest ()
		{
			ScApiSession session = this.instanceSettings.GetSession();
			string itemId = itemIdTextField.Text;
			ScItemsResponse response = await session.GetItemById(itemId);
			if (response.ResultCount > 0)
			{
				ScItem item = response.Items [0];
				AlertHelper.ShowAlert ("Item received", "item title is \"" + item.DisplayName + "\"", "OK");
			}
			else
			{
				AlertHelper.ShowErrorAlertWithOkOption("Message", "Item is not exist");
			}
		}
	}
}

