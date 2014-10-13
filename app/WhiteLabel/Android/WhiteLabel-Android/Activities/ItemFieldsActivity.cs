namespace WhiteLabelAndroid.SubActivities
{
  using Android.App;
  using Android.OS;
  using Android.Views;

  [Activity(Label = "ItemFieldsActivity")]
  public class ItemFieldsActivity : Activity
  {
    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      ActionBar.SetDisplayHomeAsUpEnabled(true);
    }

    public override bool OnOptionsItemSelected(IMenuItem item)
    {
      switch (item.ItemId)
      {
        case Android.Resource.Id.Home:
          this.Finish();
          return true;
      }
      return base.OnOptionsItemSelected(item);
    }
  }
}