namespace WhiteLabelAndroid.Activities.Create
{
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Android.Widget;

  [Activity]
  public abstract class BaseCreateItemActivity : Activity
  {
    #region Views
    protected EditText ItemField;
    protected EditText ItemNameField;
    protected EditText ItemTitleFieldValue;
    protected EditText ItemTextFieldValue;
    #endregion

    protected Prefs Prefs;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      this.SetContentView(Resource.Layout.activity_create_item);

      this.Prefs = Prefs.From(this);

      this.ItemField = this.FindViewById<EditText>(Resource.Id.field_item);

      this.ItemNameField = this.FindViewById<EditText>(Resource.Id.field_item_name);

      this.ItemTitleFieldValue = this.FindViewById<EditText>(Resource.Id.field_item_title_field_name);
      this.ItemTextFieldValue = this.FindViewById<EditText>(Resource.Id.field_item_text_field_value);

      var createItemButton = this.FindViewById<Button>(Resource.Id.button_create_item);
      var updateCreatedItemButton = this.FindViewById<Button>(Resource.Id.button_update_created_item);

      createItemButton.Click += (sender, args) => this.PerformCreateRequest();
      updateCreatedItemButton.Click += (sender, args) => this.PerformUpdateCreatedItemRequest();
    }

    protected abstract void PerformCreateRequest();
    protected abstract void PerformUpdateCreatedItemRequest();
  }
}