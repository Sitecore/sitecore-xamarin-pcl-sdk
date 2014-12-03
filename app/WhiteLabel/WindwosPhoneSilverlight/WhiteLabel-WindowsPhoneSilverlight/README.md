#**.Net SDK Windows Phone WhiteLabel app overview**


- [How to run WhiteLabel-WindowsPhoneSilverlight application](#HowTo) 
- [WhiteLabel-WindowsPhoneSilverlight overview](#WhiteLabel) 
- [Troubles](#Troubles) 
	* [Server side issues](#ServerSideIssues) 
	* [Application side issues](#ApplicationSideIssues) 

###<a name="HowTo">**How to run WhiteLabel-WindowsPhoneSilverlight application**
To build Windows Phone application from source code you will need:
> 1. PC with Installed Visual Studio.
> 2. Windows Phone SDK (visit [Windows Phone Silverlight development](http://msdn.microsoft.com/library/windows/apps/ff402535.aspx) for more information). </br>
> 3. If you don't use emulator register your phone for development for Windows Phone 8 ([How to register your phone for development for Windows Phone 8](http://msdn.microsoft.com/en-US/library/windows/apps/ff769508%28v=vs.105%29.aspx)). 

###Application launching:
> 1. Clone or download the following GitHub repository: https://github.com/Sitecore/sitecore-xamarin-pcl-sdk.git
2. Open the following file with Visual Studio **root**/solutions/WhiteLabel-WindowsPhoneSilverlight.sln
3. Select Debug -> Start Debugging.

###<a name="WhiteLabel">**WhiteLabel-WindowsPhoneSilverlight**

This app receives **Home** item from Sitecore instance and displays the value of item's **Text** field.
Code that receives this item is located in **MakeRequest()** method.

![Main Page](https://github.com/Sitecore/sitecore-xamarin-pcl-sdk/tree/screens/app/WhiteLabel/WindwosPhoneSilverlight/WhiteLabel-WindowsPhoneSilverlight/screens/MainPage.png?raw=true)

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

1. Build -> Clean Solution
2. Build -> Rebuild Solution

