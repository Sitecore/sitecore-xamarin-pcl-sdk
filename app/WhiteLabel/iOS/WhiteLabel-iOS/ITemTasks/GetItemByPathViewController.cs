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
			Title = NSBundle.MainBundle.LocalizedString ("getItemByPath", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			this.itemPathField.ShouldReturn = this.HideKeyboard;
		}

		partial void getItem (MonoTouch.UIKit.UIButton sender)
		{
			if (String.IsNullOrEmpty(itemPathField.Text))
			{
				AlertHelper.ShowAlertWithOkOption("Error", "Please type item Id");
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
				string itemPath = itemPathField.Text;
				this.ShowLoader ();

				ScItemsResponse response = await session.ReadItemByPathAsync (itemPath);

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

