namespace WhiteLabelAndroid.Activities
{
  using System.Collections.Generic;
  using System.Linq;
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Android.Views.InputMethods;
  using Android.Widget;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public abstract class BaseReadItemActivity : Activity, AdapterView.IOnItemClickListener
  {
    public static ISitecoreItem SelectedItem { get; set; }

    protected Prefs prefs;
    
    #region Views
    private RadioGroup payloadRadioGroup;
    private ListView itemsListView;

    private CheckBox scopeParentCheckBox;
    private CheckBox scopeSelfCheckBox;
    private CheckBox scopeChildrenCheckBox;
    private LinearLayout scopeContainer;

    protected EditText fieldNamEditText;

    protected TextView itemFieldLabel;
    protected EditText ItemFieldEditText;

    protected Button getItemsButton;
    #endregion

    private IEnumerable<ISitecoreItem> items;
    

    protected void HideKeyboard(View view)
    {
      var inputMethodManager = this.GetSystemService(InputMethodService) as InputMethodManager;
      inputMethodManager.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
    }

    protected override void OnCreate(Bundle savedInstanceState)
    {
      base.OnCreate(savedInstanceState);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      this.SetContentView(Resource.Layout.SimpleItemLayout);

      this.prefs = Prefs.From(this);

      this.payloadRadioGroup = this.FindViewById<RadioGroup>(Resource.Id.group_payload_type);
      
      this.itemsListView = this.FindViewById<ListView>(Resource.Id.items_list);
      this.itemsListView.OnItemClickListener = this;

      this.scopeParentCheckBox = this.FindViewById<CheckBox>(Resource.Id.checkbox_scope_parent);
      this.scopeSelfCheckBox = this.FindViewById<CheckBox>(Resource.Id.checkbox_scope_self);
      this.scopeChildrenCheckBox = this.FindViewById<CheckBox>(Resource.Id.checkbox_scope_children);

      this.scopeContainer = this.FindViewById<LinearLayout>(Resource.Id.container_scope);

      this.fieldNamEditText = this.FindViewById<EditText>(Resource.Id.field_item_field);

      this.ItemFieldEditText = this.FindViewById<EditText>(Resource.Id.field_item);
      this.itemFieldLabel = this.FindViewById<TextView>(Resource.Id.label);

      this.getItemsButton = this.FindViewById<Button>(Resource.Id.button_get_item);
    }

    protected IEnumerable<ScopeType> GetSelectedScopes()
    {
      var scopes = new List<ScopeType>();

      if (this.scopeParentCheckBox.Checked)
      {
        scopes.Add(ScopeType.Parent);
      }

      if (this.scopeSelfCheckBox.Checked)
      {
        scopes.Add(ScopeType.Self);
      }

      if (this.scopeChildrenCheckBox.Checked)
      {
        scopes.Add(ScopeType.Children);
      }

      return scopes;
    }

    protected PayloadType GetSelectedPayload()
    {
      switch (this.payloadRadioGroup.CheckedRadioButtonId)
      {
        case Resource.Id.payload_min:
          return PayloadType.Min;
        case Resource.Id.payload_content:
          return PayloadType.Content;
        case Resource.Id.payload_full:
          return PayloadType.Full;
        default:
          return PayloadType.Min;
      }
    }

    protected void PopulateItemsList(IEnumerable<ISitecoreItem> items)
    {
      this.items = items;

      var count = items.Count();
      var listItems = new string[count];

      for (int i = 0; i < count; i++)
      {
        ISitecoreItem item = items.ElementAt(i);
        listItems[i] = item.DisplayName;
      }
      this.itemsListView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, listItems);
    }

    public void OnItemClick(AdapterView parent, View view, int position, long id)
    {
      SelectedItem = this.items.ToArray()[position];

      StartActivity(typeof(ItemFieldsActivity));
    }
  }
}