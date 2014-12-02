#**.Net SDK Android WhiteLabel app overview**


- [How to run WhiteLabel-Android application](#HowTo) 
- [WhiteLabel-Android overview](#WhiteLabel) 
	* [Main Page](#MainPage) 
	* [Settings](#Settings) 
	* [Get Item by ID](#GetItembyID) 
	* [Get Item by Path](#GetItembyPath) 
	* [Get Item by Query](#GetItembyQuery) 
	* [Create/Update Item by ID](#Create.UpdateItembyID) 
	* [Delete Item](#DeleteItem) 
	* [Authentication](#Authentication) 
	* [Download Image](#DownloadImage) 
	* [Upload Image](#UploadImage) 
	* [Get Rendering HTML](#GetRenderingHTML) 
- [Troubles](#Troubles) 
	* [Server side issues](#ServerSideIssues) 
	* [Application side issues](#ApplicationSideIssues) 

###<a name="HowTo">**How to run WhiteLabel-Android application**
To build Android application from source code you will need:
> 	1. PC with Installed Xamarin Studio (free trial, you can download it from the [xamarin](http://xamarin.com/)).

###Application launching:
> 1. Clone or download the following GitHub repository: https://github.com/Sitecore/sitecore-xamarin-pcl-sdk.git
2. Open the following file with Xamarin studio **root**/solutions/WhiteLabel-Android.sln
3. Select Run -> Run With -> Choose simulator

###<a name="WhiteLabel">**WhiteLabel-Android overview**
####<a name="MainPage">*Main Page*
On the main page you can see the list of all available features. To see feature screen you should touch appropriate element.

![Main Page](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/MainPage.png?raw=true)

####<a name="Settings">*Settings*

Touch  ![Settings Icon](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/SettingsIcon.png?raw=true) button to open Settings screen.

![Settings Screen](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/SettingsScreen.png?raw=true)

*Options:*

1. Instance Url. Required. Your CMS instance url, should be started with ‘http://’ or ‘https://’ prefix.
2. Login. Required. The username of the existent user.
3. Password. Required. The password of the existent user.
4. Site. Optional. Can be empty, but you must specify domain for the username in this case. 
For example, username should be like: ‘sitecore\admin’.
5. Database. Optional. The default database to retrieve items from.

####<a name="GetItembyID">*Get Item by ID*

![Get Item By Id Screen](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/GetItemById.png?raw=true)

*Options:*

1. Appropriate item GUID values enclosed in curly braces. Required.
2. Field name. Optional. Will read single field value.
3. Payload. Required. Specified the set of fields to read.
4. Scope. Required. Specified item, or parent item, or item’s children will be read.

Touch “Get Item” button to read item. Result should look like this:

![Get Item By Id Results Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/GetItemByIdResults.png?raw=true)

To see fields list just touch the item. For example, ‘Home’ item have following fields:

![Get Item By Id Item Fields Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/GetItemByIdResultsFields.png?raw=true)

Touch ‘Back’ button to return to the main screen.

Source code can be found in :

| Request 	| **ReadItemsRequestWithId** |
|---------	| ---------------------------	|
| Class 	| *ReadItemByIdActivity* 	|
| Method 	| *PerformGetItemRequest(string id)* 	|

####<a name="GetItembyPath">*Get Item by Path*

![Get Item By Path Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/GetItemByPath.png?raw=true)

*Options:*

1. .Appropriate item path. Required.
2. Field name. Optional. Will read single field value.
3. Payload. Required. Specified the set of fields to read.
4. Scope. Required. Specified item, or parent item, or item’s children will be read.

Touch “Get Item” button to read item. You will receive same results as in section [Get Item by ID](#GetItembyID) 

Source code can be found in :

| Request 	| **ReadItemsRequestWithPath** |
|---------	| ---------------------------	|
| Class 	| *ReadItemByPathActivity* 	|
| Method 	| *PerformGetItemRequest(string path)* 	|

####<a name="GetItembyQuery">*Get Item by Query*

![Get Item by Query Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/GetItemByQuery.png?raw=true)

*Options:*

1. Sitecore Query. Required. 

Touch “Get Item” button to read item. With default query in this demo you will receive children list for the ‘Home’ item.

![Get Item by Query Results Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/GetitemByQueryResults.png?raw=true)

Source code can be found in :

| Request 	| **ReadItemsRequestWithSitecoreQuery** |
|---------	| ---------------------------	|
| Class 	| *ReadItemByQueryActivtiy* 	|
| Method 	| *PerformGetItemRequest(string query)* 	|

####<a name="Create.UpdateItembyID">*Create/Update Item by ID*

![Create Item By Id Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/CreateItemById.png?raw=true)

*Options:*

1. Parent item id. Required.
2. New Item Display name. Required.
3. New Item Title Field value. Required.
4. New Item Text Field value. Required.

To create new item touch ‘create’ button.
When new item will be created the following message will appear.

![Create Item By Path Results Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/CreateItemByIdResult.png?raw=true)

And the appropriate item will be created:

![Created Item Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/CreatedItem.png?raw=true)

Also you can update created item, to do this you need change some of the fields values and touch ‘Update created item’.
For example, type ‘Updated title’ instead of ‘Test title’ and touch ‘Update created item’. Appropriate item will be updated:

![Updated Item Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/UpdatedItem.png?raw=true)

Source code can be found in :

| Request 	| **CreateItemRequestWithParentPath** |
|---------	| ---------------------------	|
| Class 	| *CreateItemByIdActivity* 	|
| Method 	| *PerformCreateRequest()* 	|

| Request 	| **UpdateItemRequestWithPath** |
|---------	| ---------------------------	|
| Class 	| *CreateItemByIdActivity* 	|
| Method 	| *PerformUpdateCreatedItemRequest()* 	|

####<a name="DeleteItem">*Delete Item*

![Delete Item Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/DeleteItem.png?raw=true)

*Options:*

1. Item GUID values enclosed in curly braces. Required.
2. Item path. Required.
3. Sitecore query. Required.

You have 3 option to delete items. Fill one of the fields and touch button under field, appropriate item will be deleted. And you should see the following alert.

![Delete Item Results Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/DeleteItemResults.png?raw=true)

Source code can be found in :

| Request 	| **DeleteItemRequestWithId** |
|---------	| ---------------------------	|
| Class 	| *DeleteItemActivity* 	|
| Method 	| *DeleteItemById()* 	|

| Request 	| **DeleteItemRequestWithPath** |
|---------	| ---------------------------	|
| Class 	| *DeleteItemActivity* 	|
| Method 	| *DeleteItemByPath()* 	|

| Request 	| **DeleteItemRequestWithSitecoreQuery** |
|---------	| ---------------------------	|
| Class 	| *DeleteItemActivity* 	|
| Method 	| *DeleteItemByQuery()* 	|

####<a name="Authentication">*Authentication*

![Authentication Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/Authentication.png?raw=true)

*Options:*

1. Instance Url. Required. Your CMS instance url, should be started with ‘http://’ or ‘https://’ prefix.
2. Login. Required. The username of the existent user.
3. Password. Required. The password of the existent user.
4. Site. Optional. Can be empty, but you must specify domain for the user name in this case. For example, user name should be like: ‘sitecore\admin’.

As result you will see the following alert if this user exists. 

![Authentication Result Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/AuthenticationResult.png?raw=true)

Source code can be found in :

| Request 	| **AuthenticatedSessionWithHost** |
|---------	| ---------------------------	|
| Class 	| *AuthenticateActivity* 	|
| Method 	| *Authenticate()* 	|

####<a name="DownloadImage">*Download Image*

![Download Image Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/DownloadImage.png?raw=true)

*Options:*

1. Media path. Required.
2. Image width. Optional.
3. Image height. Optional.

As result you will see the requested image.

![Download Image Result Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/DownloadImageResult.png?raw=true)

Source code can be found in :

| Request 	| **DownloadResourceRequestWithMediaPath** |
|---------	| ---------------------------	|
| Class 	| *DownloadImageActivtiy* 	|
| Method 	| *DownloadImage(string itemPath, string widthStr, string heightStr)* 	|

####<a name="UploadImage">*Upload Image*

![Upload Image Result Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/UploadImage.png?raw=true)

*Options:*

1. Tap “Select Image” to choose image for uploading.
2. New image name. Required.
3. Path to images folder. Required. Related to the media library root.

Touch ‘Upload Image’ button to start image uploading. As a result you will see the following alert:

![Uploaded Image Result Screen](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/UploadImageResult.png?raw=true)

Source code can be found in :

| Request 	| **UploadResourceRequestWithParentPath** |
|---------	| ---------------------------	|
| Class 	| *UploadImageActivity* 	|
| Method 	| *UploadImage(Android.Net.Uri data)* 	| 

####<a name="GetRenderingHTML">*Get Rendering HTML*

![Get Rendering HTML Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/GetRenderingHtml.png?raw=true)

*Options:*

1. Data source item GUID  for rendering, values enclosed in curly braces. Required.
2. Rendering item GUID, values enclosed in curly braces. Required.

Touch ‘Get rendering html’ button to get rendering. As result you will see the html page.
For example, use: <br/>
>*{110D559F-DEA5-42EA-9C1C-8A5DF7E70EF9}* - Home item for source id.<br/>
>*{493B3A83-0FA7-4484-8FC9-4680991CF743}* - default Sitecore ‘Sample Rendering’ for rendering id.

Result:

![Get Rendering HTML Result Screen ](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/blob/screens/app/WhiteLabel/Android/WhiteLabel-Android/screens/GetRenderingHtmlResult.png?raw=true)

Source code can be found in :

| Request 	| **RenderingHtmlRequestWithSourceAndRenderingId** |
|---------	| ---------------------------	|
| Class 	| *GetRenderingHtmlActivity* 	|
| Method 	| *GetRenderingHtml()* 	| 

###<a name="Troubles">**Troubles**

####<a name="ServerSideIssues">*Server side issues*

In case of any errors from server side try to ensure that you have correctly installed and configured WebApi module on your cms. Also ensure you have access to the server from your device or simulator. Try to send the following request via your browser(simulator browser, device browser):

```
<host>/-/item/v1%2fsitecore%2fshell/-/actions/getpublickey
```

response should look like:
```xml
<RSAKeyValue>Modulus>qj4TwhUdSCSCYo8g4o/bWMCyI3NiNLAB79NvU6rdEGS4U1u9DNd3LUThqKBY7OqsL8A5dE6HE+0y95BXVmlmb9FSQPpwygnVl0C+Ym+ahRafNBcmf04wYuwV6OWsnA7RtKWT3c0xpuYmxiUoqghrSLbk+QjtmRnBxfsN4qJjHuU=</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>
```

####<a name="ApplicationSideIssues">*Application side issues*

In case of application errors try to cleanup application and rebuild all components:

1. Build -> Clean All
2. Build -> Rebuild All
