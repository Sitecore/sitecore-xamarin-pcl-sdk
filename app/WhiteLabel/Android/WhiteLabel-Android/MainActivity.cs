namespace WhiteLabelAndroid
{
    using Android.App;
    using Android.Content;
    using Android.OS;
    using Android.Views;
    using Android.Widget;
    using WhiteLabelAndroid.SubActivities;

    [Activity(Label = "WhiteLabel-Android", MainLauncher = true)]
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

            Button itemIdButton = this.FindViewById<Button>(Resource.Id.button_get_item_by_id);
            itemIdButton.Click += async (sender, e) => this.StartActivity(new Intent(this, typeof(GetItemByIdActivity)));

            Button itemPathButton = this.FindViewById<Button>(Resource.Id.button_get_item_by_path);
            itemPathButton.Click += async (sender, e) => this.StartActivity(new Intent(this, typeof(GetItemByPathActivtiy)));

            Button itemQueryButton = this.FindViewById<Button>(Resource.Id.button_get_item_by_query);
            itemQueryButton.Click += async (sender, e) => this.StartActivity(new Intent(this, typeof(GetItemByQueryActivtiy)));
        }
    }
}