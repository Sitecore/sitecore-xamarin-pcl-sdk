
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

namespace WhiteLabeliOS
{
	public partial class BaseTaskViewController : UIViewController
	{
		public InstanceSettings instanceSettings {get; set;}

		public BaseTaskViewController (IntPtr handle) : base (handle)
		{

		}

		public bool HideKeyboard(MonoTouch.UIKit.UITextField sender)
		{
			sender.ResignFirstResponder();
			return true;
		}
	}
}

