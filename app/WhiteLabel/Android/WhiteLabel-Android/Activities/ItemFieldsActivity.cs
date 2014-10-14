namespace WhiteLabelAndroid.Activities
{
  using System.Collections.Generic;
  using System.Linq;
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Android.Widget;
  using Sitecore.MobileSDK.API.Fields;
  using Sitecore.MobileSDK.API.Items;
  using WhiteLabelAndroid.Activities.Read;

  [Activity]
  public class ItemFieldsActivity : ListActivity
  {
    private ISitecoreItem selectedItem;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.ActionBar.SetDisplayHomeAsUpEnabled(true);
      this.selectedItem = BaseReadItemActivity.SelectedItem;

      Title = this.selectedItem.DisplayName;
      this.InitList(this.selectedItem.Fields);
    }

    private void InitList(IEnumerable<IField> fields)
    {
      var count = fields.Count();
      var listFields = new string[count];

      for (int i = 0; i < count; i++)
      {
        var field = fields.ElementAt(i);

        listFields[i] = field.Name;
      }
      this.ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listFields);
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

    protected override void OnListItemClick(ListView l, View v, int position, long id)
    {
      DialogHelper.ShowSimpleDialog(this, this.selectedItem.Fields.ElementAt(position).Name,
        this.selectedItem.Fields.ElementAt(position).RawValue);
    }
  }
}