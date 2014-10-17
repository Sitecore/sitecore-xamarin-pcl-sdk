namespace WhiteLabelAndroid.Activities.Media
{
  using System;
  using System.IO;
  using Android.App;
  using Android.Content;
  using Android.Database;
  using Android.Graphics;
  using Android.OS;
  using Android.Provider;
  using Android.Views;
  using Android.Widget;
  using Java.IO;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;

  [Activity]
  public class UploadImageActivity : Activity
  {
    private ImageView selectedImage;
    private Android.Net.Uri imageUri;

    private Bitmap imageBitmap;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      SetContentView(Resource.Layout.activity_upload_image);

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
        this.imageUri = data.Data;
        try
        {
          if (this.imageBitmap != null)
            this.imageBitmap.Recycle();

          this.imageBitmap = MediaStore.Images.Media.GetBitmap(ContentResolver, data.Data);
          this.selectedImage.SetImageBitmap(this.imageBitmap);
        }
        catch (Exception)
        {
          Toast.MakeText(this, "Image is to large for preview", ToastLength.Long).Show();
        }
      }
    }

    private string GetPathToImage(Android.Net.Uri uri)
    {
      string path = null;

      var projection = new[] { Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data };
      using (ICursor cursor = this.ContentResolver.Query(uri, projection, null, null, null))
      {
        if (cursor != null)
        {
          int columnIndex = cursor.GetColumnIndexOrThrow(Android.Provider.MediaStore.Images.Media.InterfaceConsts.Data);
          cursor.MoveToFirst();
          path = cursor.GetString(columnIndex);
        }
        else
        {
          return uri.Path;
        }
      }
      return path;
    }

    private async void UploadImage(Android.Net.Uri data)
    {
      var imageNameField = this.FindViewById<EditText>(Resource.Id.field_media_item_name);
      var imagePathField = this.FindViewById<EditText>(Resource.Id.field_media_item_path);

      var imageName = imageNameField.Text;
      var imagePath = imagePathField.Text;

      if (string.IsNullOrWhiteSpace(imageName))
      {
        Toast.MakeText(this, "Please select image name before upload", ToastLength.Long).Show();
        return;
      }

      if (data == null)
      {
        Toast.MakeText(this, "Please select image before upload", ToastLength.Long).Show();
        return;
      }

      var imageFilePath = this.GetPathToImage(data);

      if (imageFilePath == null)
      {
        Toast.MakeText(this, "Failed to upload image", ToastLength.Long).Show();
        return;
      }

      try
      {
        this.SetProgressBarIndeterminateVisibility(true);

        using (ISitecoreWebApiSession session = Prefs.From(this).Session)
        {
          using (Stream stream = System.IO.File.Open(imageFilePath, FileMode.Open))
          {
            var builder = ItemWebApiRequestBuilder.UploadResourceRequestWithParentPath(imagePath)
              .ItemDataStream(stream)
              .ContentType("image/jpg")
              .ItemName(imageName)
              .FileName("bugaga.jpg");

            var response = await session.UploadMediaResourceAsync(builder.Build());

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