namespace MobileSDKIntegrationTest
{
  using System;
  using System.Threading.Tasks;
  using NUnit.Framework;
  using Sitecore.MobileSDK.API;
  using Sitecore.MobileSDK.API.Items;
  using Sitecore.MobileSDK.API.Session;

  [TestFixture]
  public class GetRenderingHtmlTest
  {
    private TestEnvironment testData;
    private ISitecoreWebApiReadonlySession sessionAuthenticatedUser;

    private const string ItemWithSpacesPath = "/sitecore/content/Home/Android/Static/Test item 1";
    private const string ItemWithSpacesName = "Test item 1";

    [SetUp]
    public void Setup()
    {
      testData = TestEnvironment.DefaultTestEnvironment();
      this.sessionAuthenticatedUser =
        SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(this.testData.InstanceUrl)
          .Credentials(this.testData.Users.Admin)
          .BuildReadonlySession();
    }

    [TearDown]
    public void TearDown()
    {
      this.sessionAuthenticatedUser.Dispose();
      this.sessionAuthenticatedUser = null;
      this.testData = null;
    }

    //    [Test]
    //    public async void TestGetMediaItem()
    //    {
    //      // @igk !!! TEMPORARY TEST FOR CUSTOM USE, DO NOT DELETE, PLEASE !!!
    //      IDownloadMediaOptions options = new MediaOptionsBuilder().Set
    //        .SetDisplayAsThumbnail(true)
    //        .Build();
    //
    //      var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/testname222")
    //        .DownloadOptions(options)
    //        .Database("master")
    //        .Build();
    //        
    //      var response = await this.sessionAuthenticatedUser.DownloadResourceAsync(request);
    //     
    //      byte[] data;
    //
    //      using (BinaryReader br = new BinaryReader(response))
    //      {
    //        data = br.ReadBytes((int)response.Length);
    //      }
    //
    //      UIImage image = null;
    //      image = new UIImage(NSData.FromArray(data));
    //      //string text = reader.ReadToEnd();
    //
    //      var documentsDirectory = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
    //      string jpgFilename = System.IO.Path.Combine (documentsDirectory, "Photo.jpg"); // hardcoded filename, overwrites each time
    //      NSData imgData = image.AsJPEG();
    //      NSError err = null;
    //      if (imgData.Save(jpgFilename, false, out err))
    //      {
    //        Console.WriteLine("saved as " + jpgFilename);
    //      } else {
    //        Console.WriteLine("NOT saved as " + jpgFilename + " because" + err.LocalizedDescription);
    //      }
    //     
    //    }

    [Test]
    public async void TestGetRendering()
    {
      var request = ItemWebApiRequestBuilder.RenderingHtmlRequestWithSourceAndRenderingId("{138C6674-A29C-4674-9666-F9126E34B99D}","{133C5863-D020-4CDF-9AA3-4ED0015F2F30}")
        .Build();
      string response = await this.sessionAuthenticatedUser.ReadRenderingHtmlAsync(request);
      string expected = "<div style=\"display: inline-block;\" xmlns:scmobile=\"http://www.sitecore.net/scmobile\"><script type=\"text/javascript\"> \r\n        function scmobile_send_email_ID0EHBEA()\r\n        {\r\n\t\t\r\n          function htmlDecode( str )\r\n          {\r\n\t\t    if ( str.length == 0 )\r\n\t\t\t   return '';\r\n            var div = document.createElement('div');\r\n            div.innerHTML  = str;\r\n            return div.firstChild.nodeValue;\r\n          }\r\n\r\n\t\t  var email = new scmobile.share.Email();\r\n\t\t  \r\n          var brTagStr_ = unescape('%3C%62%72%2F%3E');\r\n          email.toRecipients  = htmlDecode( '' ).split(brTagStr_);\r\n          email.ccRecipients  = htmlDecode( '' ).split(brTagStr_);\r\n          email.bccRecipients = htmlDecode( '' ).split(brTagStr_);\r\n          email.subject       = htmlDecode( '' );\r\n          \r\n\t\t  var localBody = '';\r\n\t\t  var loc = window.location;\r\n\t\t  var hostPrefix = loc.protocol + '//' + loc.hostname + ':' + loc.port;\r\n\t\t  \r\n\t\t  var fullMediaPrefix = '\"' + hostPrefix + '/~/';\r\n\t\t  var exists = false;\r\n\t\t  if (localBody.indexOf('\"/~/') > 0)\r\n\t\t\t\texists = true;\r\n\t\t  while (exists)\r\n\t\t  {\r\n\t\t\tlocalBody = localBody.replace('\"/~/', fullMediaPrefix);\r\n\r\n\t\t\tif (localBody.indexOf('\"/~/') > 0)\r\n\t\t\t\texists = true;\r\n\t\t\telse\r\n\t\t\t\texists = false;\r\n\t\t  }\r\n\t\t  email.messageBody = localBody;\r\n\t\t  \r\n          email.isHTML = true;\r\n\t\t  \r\n          function onSuccess( data )\r\n          {\r\n              scmobile.console.log('onSuccess: ' + data.result);\r\n          }\r\n\r\n          function onError( data )\r\n          {\r\n            scmobile.console.log('onError: ' + data.error);\r\n          }\r\n\r\n          email.send( onSuccess, onError );\r\n        }\r\n\t  </script><a onclick=\"scmobile_send_email_ID0EHBEA()\"><img src=\"/~/media/Mobile SDK/mobile_email.ashx?h=48&amp;la=en&amp;w=48\" alt=\"\" width=\"48\" height=\"48\" /></a></div>";

      Assert.AreEqual(expected, response);
    }


  }
}