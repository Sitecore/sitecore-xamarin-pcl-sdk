using System;
using MonoTouch.UIKit;

namespace WhiteLabeliOS
{
	public class AlertHelper
	{
		public AlertHelper ()
		{
		}

		public static void ShowErrorAlertWithOkOption(string title, string message)
		{
			UIAlertView alert = new UIAlertView () { 
				Title = title, Message = message
			};
			alert.AddButton("OK");
			alert.Show ();
		}
	}
}

