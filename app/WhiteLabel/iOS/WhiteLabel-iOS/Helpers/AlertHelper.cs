using System;
using MonoTouch.UIKit;
using System.Threading.Tasks;

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
	}
}

