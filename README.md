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
* Windows Phone 8 and newer


It uses the modern C# approaches such as:
* PCL distribution
* async/await based API
* Fluent interface



# Licence
SITECORE SHARED SOURCE LICENSE


# Adding the SDK to your Project
The SDK is can be added to your project from the following sources 

* [NuGet repository](https://www.nuget.org)
* [Xamarin Store](https://components.xamarin.com)

The actual installation instructions may vary depending on both the distribution system and the IDE you are using. Most of them can by found in one of the following links :

* http://docs.nuget.org/docs/start-here/overview
* http://developer.xamarin.com/guides/cross-platform/application_fundamentals/nuget_walkthrough/
* http://developer.xamarin.com/guides/cross-platform/application_fundamentals/components_walkthrough/



# Sample Code
Since the SDK has been designed as a portable class library (PCL), you can use the very same code on all platforms to fetch the home item contents. 

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

