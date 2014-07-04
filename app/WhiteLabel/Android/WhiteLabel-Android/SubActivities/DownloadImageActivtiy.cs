using Android.App;

namespace WhiteLabelAndroid.SubActivities
{
  using Android.Content.PM;
  using Android.Graphics;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using Java.Lang;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.UrlBuilder.MediaItem;

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
        ScApiSession session = new ScApiSession(this.prefs.SessionConfig, this.prefs.ItemSource);

        MediaOptionsBuilder builder = new MediaOptionsBuilder();

        if (!string.IsNullOrEmpty(widthStr))
        {
          var width = Integer.ParseInt(widthStr);
          builder.SetWidth(width);
        }


        if (!string.IsNullOrEmpty(heightStr))
        {
          var height = Integer.ParseInt(heightStr);
          builder.SetHeight(height);
        }

        var request = ItemWebApiRequestBuilder.ReadMediaItemRequest(itemPath).DownloadOptions(builder.Build()).Build();

        this.SetProgressBarIndeterminateVisibility(true);
        var response = await session.DownloadResourceAsync(request);
        
        this.SetProgressBarIndeterminateVisibility(false);
        var imageBitmep = BitmapFactory.DecodeStream(response);

        this.targetImageView.SetImageBitmap(imageBitmep);
      }
      catch (System.Exception exception)
      {
        this.SetProgressBarIndeterminateVisibility(false);
        var title = GetString(Resource.String.text_item_received);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }
  }
}