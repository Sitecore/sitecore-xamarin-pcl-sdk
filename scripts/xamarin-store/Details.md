Sitecore Mobile SDK - a Portable .NET Library
========

Sitecore Mobile SDK is a framework that is designed to help the developer produce native mobile applications that use and serve content that is managed by Sitecore. The library is PCL standard compliant and can be used on the following platforms :

* iOS 7 and newer
* Android
* Windows Desktop (.NET 4.5)
* Windows Phone 7.1
* Silverlight 5

It uses the modern C# features and approaches such as :
* PCL distribution
* async/await based API
* Fluent interface


## The SDK includes the following features:

* Authentication
* CRUD operations on items
* Access item fields and properties
* Download content of media items
* Upload media items
* Getting html rendering of an item


# Licence
```
SITECORE SHARED SOURCE LICENSE
```



Since the SDK has been designed as a portable class library (PCL), you can use the very same code on all platforms to fetch the home item contents. 

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
