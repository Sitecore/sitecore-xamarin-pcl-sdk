namespace WhiteLabel_WindowsPhoneSilverlight
{
  using Microsoft.Phone.Controls;
  using Sitecore.MobileSDK;
  using Sitecore.MobileSDK.SessionSettings;
  using Sitecore.MobileSDK.UrlBuilder.QueryParameters;

  public partial class MainPage : PhoneApplicationPage
  {
    public MainPage()
    {
      InitializeComponent();
      this.MakeRequest();
    }

    private async void MakeRequest()
    {
      var config = new SessionConfig("http://mobiledev1ua1.dk.sitecore.net:722", "extranet\\adminex", "adminex");
      var session = new ScApiSession(config, null);

      var builder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home").Payload(PayloadType.Full).Build();
      var response = await session.ReadItemAsync(builder);

      if (response.Items.Count > 0)
      {
        ResultBlock.DataContext = response.Items[0].FieldWithName("Text");
      }
    }
  }
}