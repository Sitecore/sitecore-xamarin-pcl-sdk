namespace WhiteLabel_WindowsPhoneSilverlight
{
  using System;
  using System.Windows;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Request.Parameters;
  using Sitecore.MobileSDK.PasswordProvider.Interface;

  public partial class MainPage
  {
    public MainPage()
    {
      InitializeComponent();
      this.MakeRequest();
    }

    private async void MakeRequest()
    {
      using (var credentials = new Credentials("admin", "b"))
      using (var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("http://cms75.test24dk1.dk.sitecore.net/")
        .Credentials(credentials)
        .Site("/sitecore/shell")
        .BuildSession())
      {
        try
        {
          var builder = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
            .Payload(PayloadType.Full);

          var response = await session.ReadItemAsync(builder.Build());

          if (response.ResultCount != 0)
          {
            ResultBlock.DataContext = response[0]["Text"];
          }
        }
        catch (Exception exception)
        {
          MessageBox.Show("Failed to receive item. Message : " + exception.Message);
        }
      }
    }
  }

  class Credentials : IWebApiCredentials
  {
    public string Username { get; private set; }
    public string Password { get; private set; }

    public Credentials(string username, string password)
    {
      this.Username = username;
      this.Password = password;
    }

    public void Dispose()
    {
      this.Username = null;
      this.Password = null;
    }

    public IWebApiCredentials CredentialsShallowCopy()
    {
      return new Credentials(this.Username, this.Password);
    }
  }
}