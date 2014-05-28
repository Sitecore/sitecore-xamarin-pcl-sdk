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
			this.sendRequest();
		}

		private async void sendRequest ()
		{
			ScApiSession session = this.instanceSettings.GetSession();
			string itemId = itemIdTextField.Text;
			ScItemsResponse response = await session.GetItemById(itemId);
			ScItem item = response.Items [0];
			AlertHelper.ShowAlert ("result", "item title" + item.DisplayName, "ok");
		}
	}
}

