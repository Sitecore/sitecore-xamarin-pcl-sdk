namespace WhiteLabelAndroid.Activities
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
      this.MenuInflater.Inflate(Resource.Menu.main, menu);
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
      this.SetContentView(Resource.Layout.activity_main);
      this.SetTitle(Resource.String.app_name);

      var container = this.FindViewById<LinearLayout>(Resource.Id.container_buttons);

      var itemIdButton = new Button(this);
      itemIdButton.Click += (sender, e) => this.StartActivity(typeof(ReadItemByIdActivity));

      var itemPathButton = new Button(this);
      itemPathButton.Click += (sender, e) => this.StartActivity(typeof(ReadItemByPathActivtiy));

      var itemQueryButton = new Button(this);
      itemQueryButton.Click += (sender, e) => this.StartActivity(typeof(ReadItemByQueryActivtiy));

      var downloadImageButton = new Button(this);
      downloadImageButton.Click += (sender, e) => this.StartActivity(typeof(DownloadImageActivtiy));

      var createItemByIdButton = new Button(this);
      createItemByIdButton.Click += (sender, e) => this.StartActivity(typeof(DownloadImageActivtiy));

      container.AddView(itemIdButton);
      container.AddView(itemPathButton);
      container.AddView(itemQueryButton);
      container.AddView(downloadImageButton);
      container.AddView(createItemByIdButton);
    }
  }
}