namespace WhiteLabelAndroid
{
  using Android.App;
  using Android.Content;
  using Android.Content.PM;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using WhiteLabelAndroid.SubActivities;

  [Activity(MainLauncher = true, ScreenOrientation = ScreenOrientation.Portrait)]
  public class MainActivity : Activity
  {
    public override bool OnCreateOptionsMenu(IMenu menu)
    {
      MenuInflater.Inflate(Resource.Menu.main, menu);
      return base.OnCreateOptionsMenu(menu);
    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      switch (item.ItemId)
      {
        case Resource.Id.settings:
          this.StartActivity(new Intent(this, typeof(SettingsActivity)));
          break;
      }

      return true;
    }

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.SetContentView(Resource.Layout.Main);
      this.SetTitle(Resource.String.app_name);
      Button itemIdButton = this.FindViewById<Button>(Resource.Id.button_get_item_by_id);
      itemIdButton.Click += async (sender, e) => this.StartActivity(new Intent(this, typeof(ReadItemByIdActivity)));

      Button itemPathButton = this.FindViewById<Button>(Resource.Id.button_get_item_by_path);
      itemPathButton.Click += async (sender, e) => this.StartActivity(new Intent(this, typeof(ReadItemByPathActivtiy)));

      Button itemQueryButton = this.FindViewById<Button>(Resource.Id.button_get_item_by_query);
      itemQueryButton.Click += async (sender, e) => this.StartActivity(new Intent(this, typeof(ReadItemByQueryActivtiy)));

      Button downloadImageButton = this.FindViewById<Button>(Resource.Id.button_download_image);
      downloadImageButton.Click += async (sender, e) => this.StartActivity(new Intent(this, typeof(DownloadImageActivtiy)));
    }
  }
}