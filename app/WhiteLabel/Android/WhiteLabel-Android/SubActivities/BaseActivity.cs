namespace WhiteLabelAndroid.SubActivities
{
  using System.Collections.Generic;
  using Android.App;
  using Android.Content;
  using Android.OS;
  using Android.Views;
  using Android.Views.InputMethods;
  using Android.Widget;
  using Sitecore.MobileSDK.Items;
  using Sitecore.MobileSDK.Items.Fields;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public abstract class BaseActivity : Activity
  {
    protected void HideKeyboard(View view)
    {
      var inputMethodManager = this.GetSystemService(Context.InputMethodService) as InputMethodManager;
      inputMethodManager.HideSoftInputFromWindow(view.WindowToken, HideSoftInputFlags.None);
    }

    protected PayloadType GetSelectedPayload(RadioGroup payloadRadioGroup)
    {
      switch (payloadRadioGroup.CheckedRadioButtonId)
      {
        case Resource.Id.payload_min:
          return PayloadType.Min;
        case Resource.Id.payload_content:
          return PayloadType.Content;
        case Resource.Id.payload_full:
          return PayloadType.Full;
        default: return PayloadType.Min;
      }
    }

    protected void PopulateFieldsList(ListView listView, IList<IField> fields)
    {
      var items = new string[fields.Count];
      foreach (IField field in fields)
      {
        items[fields.IndexOf(field)] = field.Name;
      }

      listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
    }
  }
}