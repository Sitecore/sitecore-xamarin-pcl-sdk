The Sitecore Mobile SDK shares the main principle of the Xamarin platform. It is all about sharing business logic while creating a unique native UI for all platforms. It provides an high level abstraction for Sitecore items. The SDK lets the user operate with Items and fields rather than with HTTP requests and JSON response. 

All client-server communiaction is performed via the [Sitecore Item Web API](http://sdn.sitecore.net/Products/Sitecore%20Item%20Web%20API/SitecoreItemWebApi12.aspx) service. In order to run the sample code, you should install latest version of Sitecore CMS and make sure the mentioned service is enabled. For more details see the "Security" section of the Sitecore Item Web API [documentation](http://sdn.sitecore.net/upload/sdn5/modules/sitecore%20item%20web%20api/sitecore_item_web_api_developer_guide_sc66-71-a4.pdf).


Now you should be ready to do some content management right out of your mobile application. Run the sample application for your platform or paste the code below to a new one.


```csharp
// first we have to setup connection info and create a session
var session = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("https://my.sitecore.instance.com")
  .WebApiVersion("v1")
  .DefaultDatabase("web")
  .DefaultLanguage("en")
  .MediaLibraryRoot("/sitecore/media library")
  .MediaPrefix("~/media/")
  .DefaultMediaResourceExtension("ashx")
  .BuildSession();

// In order to fetch some data we have to build a request
var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
  .AddFields("text")
  .AddScope(ScopeType.Self)
  .Build();

// And execute it on a session asynchronously
var response = await session.ReadItemAsync(request);

// Now that it has succeeded we are able to access downloaded items
ISitecoreItem item = response.Items[0];

// And content stored it its fields
string fieldContent = item.FieldWithName("text").RawValue;
```

## Other Resources

* [Component Documentation](https://sitecore1-my.sharepoint.com/personal/adk_sitecore_net/Documents/Shared%20with%20Everyone/MobileSDK-C-sharp-Doc/Sitecore%20Mobile%20SDK%20PCL%20v1.pdf)
* [Support Forums](http://sdn.sitecore.net/Forum.aspx?)
* [Source Code Repository](http://tfs4dk1.dk.sitecore.net/tfs/PD01/Product_Mobile/_git/Xamarin_Sdk#path=%2F&version=GBmaster&_a=contents)
