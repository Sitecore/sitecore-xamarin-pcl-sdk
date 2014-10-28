Sitecore Mobile SDK for .NET
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
* Android
* Windows Desktop (.NET 4.5)
* Windows Phone 7.1


It uses the modern C# approaches such as :
* PCL distribution
* async/await based API
* Fluent interface



# Licence
SITECORE SHARED SOURCE LICENSE



# Sample Code
Since the SDK has been designed as a portable class library (PCL), you can use the very same code on all platforms to fetch the home item contents. 

```csharp
using (var credentials = new SecureStringPasswordProvider("admin", "b")) // providing secure credentials
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

# Native Versions of Mobile SDK
By default the vendors of mobile platforms restrict the developers's choice of languages, instruments and tools for mobile apps development. 
They are : 

* Objective-C and Swift - for iOS
* Java - for Android 

This SDK offers the C# API for both iOS and Android platforms. For this reason we have a dependency on the [Xamarin toolchain](http://xamarin.com/platform). If you do not want this dependency or you feel more comfortable with the platform specific tools, please use 

* [Sitecore Mobile SDK for iOS](https://github.com/sitecore/sitecore-ios-sdk/)
* [Sitecore Mobile SDK for Android](https://github.com/sitecore/sitecore-android-sdk/)



## Other Resources

* [Component Documentation](N/A)
* [Support Forums](http://sdn.sitecore.net/Forum.aspx?)
* [Xamarin Store Component](N/A)
* [NuGet Component](N/A)

