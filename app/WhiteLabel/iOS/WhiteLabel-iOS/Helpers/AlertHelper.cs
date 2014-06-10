using System;
using MonoTouch.UIKit;
using System.Threading.Tasks;
using MonoTouch.Foundation;

namespace WhiteLabeliOS
{
	public class AlertHelper
	{
		public AlertHelper ()
		{
		}

		public static void ShowAlertWithOkOption(string title, string message)
		{
			UIAlertView alert = new UIAlertView () { 
				Title = title, Message = message
			};
			alert.AddButton("OK");
			alert.Show ();
		}

		public static void ShowLocalizedAlertWithOkOption(string title, string message)
		{
			string localizedTitle = NSBundle.MainBundle.LocalizedString (title, null);
			string localizedMessage = NSBundle.MainBundle.LocalizedString (message, null);
			AlertHelper.ShowAlertWithOkOption(localizedTitle, localizedMessage);
		}

		public static Task<int> ShowAlert(string title, string message, params string [] buttons)
		{
			var tcs = new TaskCompletionSource<int> ();
			var alert = new UIAlertView 
			{
				Title = title,
				Message = message
			};
			foreach (var button in buttons) 
			{
				alert.AddButton (button);
			}

			alert.Clicked += (s, e) => tcs.TrySetResult (e.ButtonIndex);
			alert.Show ();
			return tcs.Task;
		}

		public static void ShowLocalizedNotImlementedAlert()
		{
			string title = NSBundle.MainBundle.LocalizedString ("Alert", null);
			string message = NSBundle.MainBundle.LocalizedString ("Not implemented yet", null);
			AlertHelper.ShowAlertWithOkOption(title, message);
		}
	}
}

