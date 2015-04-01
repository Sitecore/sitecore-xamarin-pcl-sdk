Sitecore Mobile SDK for Xamarin
========

Sitecore Mobile SDK is a framework that is designed to help the developer produce native mobile applications that use and serve content that is managed by Sitecore. The framework enables developers to rapidly develop applications utilizing their existing .NET development skill sets. 
The SDK includes the following features:

* Fetching CMS Content
* Source HTML renderings from Sitecore CMS
* Create, Delete, Update Items
* Downloading and Uploading Media Resources
* Protect security sensitive data

The library is PCL standard compliant and can be used on the following platforms :

* iOS 7 and newer
* Android 4.0 and newer
* Windows Desktop (.NET 4.5)
* Windows Phone 8 and newer

# Downloads
- [NuGet packages](https://www.nuget.org/packages?q=Sitecore+Mobile+SDK)
- [Xamarin component](https://components.xamarin.com/view/sitecore.mobile.sdk?version=1.0)

# Links
- [API Documentation](http://sitecore.github.io/sitecore-xamarin-pcl-sdk/v1.0/)
- [Documentation](https://doc.sitecore.net)
- [iOS Sample](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/tree/master/app/WhiteLabel/iOS)
- [Android Sample](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/tree/master/app/WhiteLabel/Android/WhiteLabel-Android)
- [Windwos Phone Silverlight](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/tree/master/app/WhiteLabel/WindwosPhoneSilverlight/WhiteLabel-WindowsPhoneSilverlight)


# Code Snippet
```csharp
using (var credentials = new SecureStringPasswordProvider("username", "password")) // providing secure credentials
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
  // In order to fetch some data we have to build a request
  var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
  .AddFieldsToRead("text")
  .AddScope(ScopeType.Self)
  .Build();

  // And execute it on a session asynchronously
  var response = await session.ReadItemAsync(request);

  // Now that it has succeeded we are able to access downloaded items
  ISitecoreItem item = response[0];

  // And content stored it its fields
  string fieldContent = item["text"].RawValue;
}
```
# Licence
SITECORE SHARED SOURCE LICENSE