
using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Sitecore.MobileSDK;
using Sitecore.MobileSDK.UrlBuilder.MediaItem;
using System.IO;

namespace WhiteLabeliOS
{
	public partial class GetMediaItemViewController : BaseTaskViewController
	{
		public GetMediaItemViewController (IntPtr handle) : base (handle)
		{
			Title = NSBundle.MainBundle.LocalizedString ("getMediaItem", null);
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			string ButtonTitle = NSBundle.MainBundle.LocalizedString ("Download", null);
			this.DownloadButton.SetTitle (ButtonTitle, UIControlState.Normal);

		}

		partial void OnDownloadButtonTouched (MonoTouch.Foundation.NSObject sender)
		{
			if (String.IsNullOrEmpty(this.MediaPathTextField.Text))
			{
				AlertHelper.ShowLocalizedAlertWithOkOption("Error", "Please type media path");
			}
			else
			{
				this.HideKeyboard(this.MediaPathTextField);

				this.SendRequest();
			}
		}

		private async void SendRequest ()
		{
			try
			{
				ScApiSession session = this.instanceSettings.GetSession();

				IDownloadMediaOptions options = new MediaOptionsBuilder()
					.SetWidth((int) Math.Round (this.ImageView.Bounds.Width))
					.SetHeight((int) Math.Round (this.ImageView.Bounds.Height))
					.SetBackgroundColor("white")
					.Build();

				string path = this.MediaPathTextField.Text;

				var request = ItemWebApiRequestBuilder.ReadMediaItemRequest(path)
					.DownloadOptions(options)
					.Build();

				var response = await session.DownloadResourceAsync(request);

				byte[] data;

				using (BinaryReader br = new BinaryReader(response))
				{
					data = br.ReadBytes((int)response.Length);
				}

				UIImage image = null;
				image = new UIImage(NSData.FromArray(data));

				BeginInvokeOnMainThread(delegate
				{
					this.ImageView.Image = image;
					this.HideLoader();
				});

			}
			catch(Exception e) 
			{
				AlertHelper.ShowLocalizedAlertWithOkOption("Erorr", e.Message);
			}
			finally
			{
				BeginInvokeOnMainThread(delegate
				{
					this.HideLoader();
				});
			}
		}
	}
}

