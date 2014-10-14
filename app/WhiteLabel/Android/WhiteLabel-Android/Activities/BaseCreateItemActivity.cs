namespace WhiteLabelAndroid.Activities
{
  using Android.App;
  using Android.OS;
  using Android.Widget;

  [Activity]
  public abstract class BaseCreateItemActivity : Activity
  {
    #region Views
    protected EditText ItemField;
    protected EditText ItemNameField;
    protected EditText FieldNameField;
    protected EditText FieldValueField;
    #endregion

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      SetContentView(Resource.Layout.activity_create_item);

      this.ItemField = this.FindViewById<EditText>(Resource.Id.field_item);

      this.ItemNameField = this.FindViewById<EditText>(Resource.Id.field_item_name);
      this.FieldNameField = this.FindViewById<EditText>(Resource.Id.field_item_field_name);
      this.FieldValueField = this.FindViewById<EditText>(Resource.Id.field_item_field_value);

      var createItemButton = this.FindViewById<Button>(Resource.Id.button_create_item);
      var updateCreatedItemButton = this.FindViewById<Button>(Resource.Id.button_update_created_item);

      createItemButton.Click += (sender, args) => this.PerformCreateRequest();
      updateCreatedItemButton.Click += (sender, args) => this.PerformUpdateCreatedItemRequest();
    }

    protected abstract void PerformCreateRequest();
    protected abstract void PerformUpdateCreatedItemRequest();
  }
}