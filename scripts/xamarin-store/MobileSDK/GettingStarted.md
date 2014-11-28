The Sitecore Mobile SDK is designed to allow content from the Sitecore Experience Platform to be used across multiple native Mobile platforms using the Xamarin platform. It provides a high level abstraction for Sitecore items and lets the developer operate with Items and fields rather than with HTTP requests and JSON responses. The SDK core conforms to the PCL (Portable Class Libraries) standard and has some platform specific plug-ins that are used to secure an end user's credentials.


All client-server communication is performed via the [Sitecore Item Web API](http://sdn.sitecore.net/Products/Sitecore%20Item%20Web%20API/SitecoreItemWebApi12.aspx) service. In order to run the sample code, you should install latest version of the Sitecore CMS and make sure that the Item Web API service is enabled. For more details see the "Security" section of the Sitecore Item Web API [documentation](http://sdn.sitecore.net/upload/sdn5/modules/sitecore%20item%20web%20api/sitecore_item_web_api_developer_guide_sc66-71-a4.pdf).


You should now be ready to access content from Sitecore within your mobile application. Run the sample application for your platform or paste the code below to a new one.


```csharp
using (var credentials = new SecureStringPasswordProvider("admin", "b")) // securing credentials, entered by the end user
using 
(
  var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost(instanceUrl)
    .Credentials(credentials)
    .WebApiVersion("v1")
    .DefaultDatabase("web")
    .DefaultLanguage("en")
    .MediaLibraryRoot("/sitecore/media library")
    .MediaPrefix("~/media/")
    .DefaultMediaResourceExtension("ashx")
    .BuildSession()
) // Creating a session from credentials, instance URL and settings
{
  // In order to fetch sample content we have to build a request
  var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
  .AddFieldsToRead("text")
  .AddScope(ScopeType.Self)
  .Build();

  // And execute it in a session asynchronously
  var response = await session.ReadItemAsync(request);

  // Now that it has succeeded we are able to access downloaded items
  ISitecoreItem item = response[0];

  // And content stored in its fields
  string fieldContent = item["text"].RawValue;
}
```

Note : Both the session and the credentials objects must be wrapped into the "using" directive since they rely on unmanaged resources of a mobile platform such as sockets and cryptography. The same should be done for all heavy objects of a native platform such as 

* Stream (.NET)
* NSData (iOS)
* UIImage (iOS)
* Bitmap (Android)
* Native collection classes (NSArray, NSDictionary, NSSet, ByteArray, etc.)
* Persistence logic (SQLite database connections)


## Other Resources

* [Component Documentation](https://sitecore1-my.sharepoint.com/personal/adk_sitecore_net/Documents/Shared%20with%20Everyone/MobileSDK-C-sharp-Doc/Sitecore%20Mobile%20SDK%20PCL%20v1.pdf)
* [Support Forums](http://sdn.sitecore.net/Forum.aspx?)
* [Source Code Repository](http://tfs4dk1.dk.sitecore.net/tfs/PD01/Product_Mobile/_git/Xamarin_Sdk#path=%2F&version=GBmaster&_a=contents)
