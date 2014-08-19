namespace WhiteLabelAndroid.SubActivities
{
  using System.Collections.Generic;
  using System.Linq;
  using Android.App;
  using Android.Views;
  using Android.Views.InputMethods;
  using Android.Widget;
  using Sitecore.MobileSDK.API.Fields;
  using Sitecore.MobileSDK.API.Request.Parameters;

  public abstract class BaseActivity : Activity
  {
    protected void HideKeyboard(View view)
    {
      var inputMethodManager = this.GetSystemService(InputMethodService) as InputMethodManager;
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

    protected void PopulateFieldsList(ListView listView, IEnumerable<IField> fields)
    {
      var count = fields.Count();
      var items = new string[count];

      for (int i = 0; i < count; i++)
      {
        IField field = fields.ElementAt(i);
        items[i] = field.Name;
      }
      listView.Adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, items);
    }
  }
}