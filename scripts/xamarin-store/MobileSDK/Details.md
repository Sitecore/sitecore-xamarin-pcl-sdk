Sitecore Mobile SDK - a Portable .NET Library
========

Sitecore Mobile SDK is a framework that is designed to help a developer produce native mobile applications that use and serve content that is managed by Sitecore. The library is compliant to PCL (Portable Class Library) standards and can be used with the following platforms:

* iOS 7 and later
* Android 4.1 and later
* Windows Desktop (.NET 4.5)
* Windows Phone 7.1
* Silverlight 5

It uses modern C# features and approaches such as :
* PCL distribution
* async/await based API
* Fluent interface


## The SDK includes the following features:

* Authentication
* Credentials protection based on SecureString class
* CRUD operations on Sitecore items
* Access Sitecore item fields and properties
* Download content of media items
* Upload media items
* Retrieve the html rendering of a Sitecore item


# Licence
```
SITECORE SHARED SOURCE LICENSE
```

## Dive in

As the SDK is designed as a portable class library (PCL), you can use the same code on all platforms to fetch the default "home" item content. 

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



