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
			AlertHelper.ShowAlertWithSingleButton (title, message, "OK");
		}

		public static void ShowAlertWithSingleButton(string title, string message, string buttonTitle)
		{
			UIAlertView alert = new UIAlertView () { 
				Title = title, Message = message
			};

			alert.AddButton(buttonTitle);
			alert.Show ();
		}

		public static void ShowLocalizedAlertWithOkOption(string title, string message)
		{
			string localizedTitle = NSBundle.MainBundle.LocalizedString (title, null);
			string localizedMessage = NSBundle.MainBundle.LocalizedString (message, null);
			string localizedButtonTitle = NSBundle.MainBundle.LocalizedString ("OK", null);

			AlertHelper.ShowAlertWithSingleButton (title, message, localizedButtonTitle);
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

