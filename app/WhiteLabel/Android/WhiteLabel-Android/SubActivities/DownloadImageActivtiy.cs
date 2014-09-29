namespace WhiteLabelAndroid.SubActivities
{
  using System;
  using Android.App;
  using Android.Content.PM;
  using Android.Graphics;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.MediaItem;
  using Sitecore.MobileSDK.API.Request.Parameters;

  [Activity(ScreenOrientation = ScreenOrientation.Portrait)]
  public class DownloadImageActivtiy : BaseActivity
  {
    private Prefs prefs;
    private ImageView targetImageView;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      this.SetContentView(Resource.Layout.DownloadImage);
      this.SetTitle(Resource.String.text_download_image);

      this.prefs = Prefs.From(this);

      var itemPathField = this.FindViewById<EditText>(Resource.Id.field_item_path);

      var imageWidth = this.FindViewById<EditText>(Resource.Id.field_image_width);
      var imageHeight = this.FindViewById<EditText>(Resource.Id.field_image_height);

      this.targetImageView = FindViewById<ImageView>(Resource.Id.downloaded_image);

      var downloadButton = this.FindViewById<Button>(Resource.Id.button_download);
      downloadButton.Click += (sender, args) =>
      {
        if (string.IsNullOrEmpty(itemPathField.Text))
        {
          DialogHelper.ShowSimpleDialog(this, Resource.String.text_error, Resource.String.text_empty_path);
          return;
        }

        this.HideKeyboard(itemPathField);
        this.DownloadImage(itemPathField.Text, imageWidth.Text, imageHeight.Text);
      };
    }

    private async void DownloadImage(string itemPath, string widthStr, string heightStr)
    {
      try
      {
        IMediaOptionsBuilder builder = new MediaOptionsBuilder().Set;

        if (!string.IsNullOrEmpty(widthStr))
        {
          var width = Int32.Parse(widthStr);
          builder.Width(width);
        }

        if (!string.IsNullOrEmpty(heightStr))
        {
          var height = Int32.Parse(heightStr);
          builder.Height(height);
        }

        var requestBuilder = ItemWebApiRequestBuilder.DownloadResourceRequestWithMediaPath(itemPath);

        IDownloadMediaOptions options = builder.Build();
        if (!options.IsEmpty)
        {
          requestBuilder.DownloadOptions(options);
        }
        
        this.SetProgressBarIndeterminateVisibility(true);

        using (var session = this.prefs.Session)
        {
          var response = await session.DownloadMediaResourceAsync(requestBuilder.Build());

          this.SetProgressBarIndeterminateVisibility(false);

          using(var imageBitmap = await BitmapFactory.DecodeStreamAsync(response))
          {
            this.targetImageView.SetImageBitmap(imageBitmap);
          }
        }
      }
      catch (Exception exception)
      {
        this.SetProgressBarIndeterminateVisibility(false);
        var title = GetString(Resource.String.text_item_received);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }
  }
}