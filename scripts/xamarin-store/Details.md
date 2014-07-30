# Sitecore Mobile SDK - a Portable .NET Library

Sitecore Mobile SDK is a framework that is designed to help the developer produce native mobile applications that use and serve content that is managed by Sitecore. The library is PCL standard compliant and can be used on the following platforms:

 - iOS 7 and newer
 - Android
 - Windows Desktop (.NET 4.5)
 - Windows Phone 7.1
 - Silverlight 5

It uses the modern C# features and approaches such as :

 - PCL distribution
 - async/await based API
 - Fluent interface

## The SDK includes following features:
--------------------------------------------

 - Authentication
 - CRUD operations on items
 - Access item fields and properties
 - Download content of media items


## Description
---------------
Since the SDK has been designed as a portable class library (PCL), the same code can be used on all platforms. 

## Code examples
---------------

### Initializing session
------------------------
#### Anonymous session
```csharp
var session = SitecoreWebApiSessionBuilder.AnonymousSessionWithHost("https://my.sitecore.instance.com")
  .WebApiVersion("v1")
  .DefaultDatabase("web")
  .DefaultLanguage("en")
  .MediaLibraryRoot("/sitecore/media library")
  .MediaPrefix("~/media/")
  .DefaultMediaResourceExtension("ashx")
  .BuildSession();
```

#### Authenticated session
```csharp
var session = SitecoreWebApiSessionBuilder.AuthenticatedSessionWithHost("https://my.sitecore.instance.com")
  .Credentials(new WebApiCredentialsPOD("username", "password");)
  .WebApiVersion("v1")
  .DefaultDatabase("web")
  .DefaultLanguage("en")
  .MediaLibraryRoot("/sitecore/media library")
  .MediaPrefix("~/media/")
  .DefaultMediaResourceExtension("ashx")
  .BuildSession();
```
------------------------
### Executing requests

#### Read item request
```csharp
var request = ItemWebApiRequestBuilder.ReadItemsRequestWithPath("/sitecore/content/home")
  .AddFields("text")
  .AddScope(ScopeType.Self)
  .Build();
  
var response = await session.ReadItemAsync(request);
```

#### Create item request
```csharp
var request = ItemWebApiRequestBuilder.CreateItemRequestWithPath("/path/to/item")
  .ItemTemplate("template")
  .ItemName("item name")
  .Database("master")
  .AddFields("title")
  .Build();
    
var response = await session.CreateItemAsync(request);
```

#### Delete item request
```csharp
var request = ItemWebApiRequestBuilder.DeleteItemRequestWithPath("/path/to/item")
  .Database("master")
  .AddScope(ScopeType.Self)
  .Build();
    
var response = await session.DeleteItemAsync(request);
```

#### Download media request
```csharp
var options = new MediaOptionsBuilder()
  .SetHeight(Height)
  .SetWidth(Width)
  .SetAllowStrech(true)
  .Build();

var request = ItemWebApiRequestBuilder.ReadMediaItemRequest("/sitecore/media library/Images/iamge_name")
  .DownloadOptions(options)
  .Database("web")
  .Language("en")
  .Build();
    
var response = await session.DownloadResourceAsync(request);
```
------------------------
### Exceptions handling

```csharp
try
{
  ICreateItemByIdRequest request = ..; //Create your request
  var response = await session.CreateItemAsync(request);
  //       Do some stuff with response
}
catch(ProcessUserRequestException)
{
  // HTTP request construction failed
}
catch(RsaHandshakeException)
{
  // Unable to encrypt user's data
}
catch(LoadDataFromNetworkException)
{
  // Connection error
}
catch(ParserException)
{
  // Server response is not valid
}
catch(WebApiJsonErrorException)
{
  // Server has returned a valid response that contains an error
}
catch(SitecoreMobileSdkException)
{
  // Some Item Web API response processing error has occurred.
  // This code should not be reached in this example.
}
catch(Exception)
{
  // Some exception has been thrown.
  // This code should not be reached in this example.
}
```
------------------------
### Code flow sample
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

# Licence
```
SITECORE SHARED SOURCE LICENSE
```
