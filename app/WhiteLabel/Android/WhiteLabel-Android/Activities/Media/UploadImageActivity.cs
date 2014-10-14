namespace WhiteLabelAndroid.Activities.Media
{
  using System.IO;
  using Android.App;
  using Android.Content;
  using Android.Database;
  using Android.Net;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;

  [Activity]
  public class UploadImageActivity : Activity
  {
    private ImageView selectedImage;
    private Uri imageUri;

    private Prefs prefs;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      SetContentView(Resource.Layout.activity_upload_image);

      this.prefs = Prefs.From(this);
      this.Title = GetString(Resource.String.text_upload_image);

      var selectImageButton = this.FindViewById<Button>(Resource.Id.button_select_image);
      var uploadImageButton = this.FindViewById<Button>(Resource.Id.button_upload_image);

      this.selectedImage = this.FindViewById<ImageView>(Resource.Id.imageview_selected_image);

      selectImageButton.Click += delegate
      {
        var imageIntent = new Intent();
        imageIntent.SetType("image/*");
        imageIntent.SetAction(Intent.ActionGetContent);
        StartActivityForResult(Intent.CreateChooser(imageIntent, "Select image"), 0);
      };

      uploadImageButton.Click += (sender, args) => this.UploadImage(this.imageUri);
    }

    protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
    {
      base.OnActivityResult(requestCode, resultCode, data);

      if (resultCode == Result.Ok)
      {
        this.selectedImage.SetImageURI(data.Data);
        this.imageUri = data.Data;
      }
    }

    private string GetPathToImage(Uri uri)
    {
      string path = null;

      var projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
      using (ICursor cursor = ManagedQuery(uri, projection, null, null, null))
      {
        if (cursor != null)
        {
          int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
          cursor.MoveToFirst();
          path = cursor.GetString(columnIndex);
        }
      }
      return path;
    }

    private async void UploadImage(Uri data)
    {
      if (data == null)
      {
        Toast.MakeText(this, "Please select image before upload", ToastLength.Long).Show();
        return;
      }

      var imagePath = this.GetPathToImage(data);

      if (imagePath == null)
      {
        Toast.MakeText(this, "Failed to upload image", ToastLength.Long).Show();
        return;
      }

      try
      {
        this.SetProgressBarIndeterminateVisibility(true);

        using (ISitecoreWebApiSession session = this.prefs.Session)
        {
          using (Stream stream = File.Open(imagePath, FileMode.Open))
          {
            var request = ItemWebApiRequestBuilder.UploadResourceRequestWithParentId("{106A23FE-9DE9-4B2D-A586-6C3846AFB33A}")
              .ItemDataStream(stream)
              .ContentType("image/jpg")
              .ItemName("ImageFromAndroid")
              .FileName("bugaga.jpg")
              .Build();

            var response = await session.UploadMediaResourceAsync(request);

            if (response != null && response.ResultCount > 0)
            {
              DialogHelper.ShowSimpleDialog(this, "Image uploaded", "Image path : " + response[0].Path);
            }
            else
            {
              var title = this.GetString(Resource.String.text_error);
              DialogHelper.ShowSimpleDialog(this, title, "Failed to upload image");
            }
          }
        }
        this.SetProgressBarIndeterminateVisibility(false);
      }
      catch (System.Exception exception)
      {
        this.SetProgressBarIndeterminateVisibility(false);

        var title = this.GetString(Resource.String.text_error);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }
  }
}