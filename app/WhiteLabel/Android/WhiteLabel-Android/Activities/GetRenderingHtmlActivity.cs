namespace WhiteLabelAndroid.Activities
{
  using System.IO;
  using Android.App;
  using Android.OS;
  using Android.Views;
  using Android.Webkit;
  using Android.Widget;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Session;

  [Activity]
  public class GetRenderingHtmlActivity : Activity
  {
    private WebView webview;

    protected override void OnCreate(Bundle bundle)
    {
      base.OnCreate(bundle);
      this.RequestWindowFeature(WindowFeatures.IndeterminateProgress);
      SetContentView(Resource.Layout.activity_rendering_html);

      this.Title = GetString(Resource.String.text_get_rendering_html);

      var getRenderingHtmlButton = this.FindViewById<Button>(Resource.Id.button_get_rendering_html);
      getRenderingHtmlButton.Click += (sender, args) => this.GetRenderingHtml();

      this.webview = this.FindViewById<WebView>(Resource.Id.webView_html);
      this.webview.Settings.JavaScriptEnabled = true;
    }

    private async void GetRenderingHtml()
    {
      var sourceIdField = this.FindViewById<EditText>(Resource.Id.field_source_id);
      var renderingIdField = this.FindViewById<EditText>(Resource.Id.field_rendering_id);

      var sourceId = sourceIdField.Text;
      var renderingId = renderingIdField.Text;

      if (string.IsNullOrWhiteSpace(sourceId))
      {
        Toast.MakeText(this, "Please enter source ID", ToastLength.Long).Show();
        return;
      }

      if (string.IsNullOrWhiteSpace(renderingId))
      {
        Toast.MakeText(this, "Please enter rendering ID", ToastLength.Long).Show();
        return;
      }

      try
      {
        this.SetProgressBarIndeterminateVisibility(true);

        using (ISitecoreWebApiSession session = Prefs.From(this).Session)
        {
          var request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId(sourceId, renderingId)
            .Build();

          var response = await session.ReadRenderingHtmlAsync(request);

          if (response != null)
          {
            var reader = new StreamReader(response);
            string html = await reader.ReadToEndAsync();

            this.webview.LoadDataWithBaseURL(null, html, null, null, null);
          }
          else
          {
            var title = this.GetString(Resource.String.text_error);
            DialogHelper.ShowSimpleDialog(this, title, "Failed load rendering html");
          }
        }
        this.SetProgressBarIndeterminateVisibility(false);
      }
      catch (System.Exception exception)
      {
        this.SetProgressBarIndeterminateVisibility(false);

        var title = this.GetString(Resource.String.text_error);
        DialogHelper.ShowSimpleDialog(this, title, exception.Message);
      }
    }
  }
}