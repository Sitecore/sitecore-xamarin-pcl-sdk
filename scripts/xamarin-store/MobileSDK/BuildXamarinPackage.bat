
SET LAUNCH_DIR=%cd%

SET XAMARIN_PACKAGE_ROOT=%cd%
SET PACKAGE_UTILITY_EXE=%XAMARIN_PACKAGE_ROOT%\xamarin-component\xamarin-component.exe

cd ..\..
SET SCRIPTS_ROOT=%cd%

cd ..
SET REPOSITORY_ROOT=%cd%

cd .\deployment
SET DEPLOYMENT_DIR=%cd%

SET BINARIES_DIR=%DEPLOYMENT_DIR%\lib

::logs
echo "==========LOGS============"
echo %PACKAGE_UTILITY_EXE%
echo %XAMARIN_PACKAGE_ROOT%
echo %SCRIPTS_ROOT%
echo %REPOSITORY_ROOT%
echo %BINARIES_DIR%
echo "==========LOGS============"
:: pause

:: And action
cd "%XAMARIN_PACKAGE_ROOT%"
echo %cd%

CALL %PACKAGE_UTILITY_EXE% create-manually Sitecore.MobileSDK.PCL-1.0.0.xam                 ^
        --name="Sitecore Mobile SDK"                                                        ^
		--summary="Use Sitecore CMS content in your native mobile apps written in C#."      ^
		--publisher="Sitecore Corporation A/S"                                              ^
		--website="http://www.sitecore.net"                                                 ^
		--details="Details.md"                                                              ^
		--license="License.md"                                                              ^
		--getting-started="GettingStarted.md"                                               ^
		--icon="icons\Sitecore.MobileSDK.PCL_128x128.png"                                   ^
		--icon="icons\Sitecore.MobileSDK.PCL_512x512.png"                                   ^
 		--library="ios":"%BINARIES_DIR%\Sitecore.MobileSDK.dll"                             ^
		--library="ios":"%BINARIES_DIR%\Sitecore.MobileSDK.PasswordProvider.iOS.dll"        ^
        --library="ios":"%BINARIES_DIR%\Sitecore.MobileSDK.PasswordProvider.Interface.dll"  ^
		--library="ios":"%BINARIES_DIR%\Microsoft.Threading.Tasks.Extensions.dll"           ^
		--library="ios":"%BINARIES_DIR%\Microsoft.Threading.Tasks.dll"                      ^
		--library="ios":"%BINARIES_DIR%\Newtonsoft.Json.dll"                                ^
		--library="ios":"%BINARIES_DIR%\System.Net.Http.Extensions.dll"                     ^
		--library="ios":"%BINARIES_DIR%\System.Net.Http.Primitives.dll"                     ^
		--library="ios":"%BINARIES_DIR%\System.Net.Http.dll"                                ^
		--library="ios":"%BINARIES_DIR%\System.Threading.Tasks.dll"                         ^
		--library="ios":"%BINARIES_DIR%\crypto.dll"                                         ^
		--library="ios":"%BINARIES_DIR%\System.IO.dll"                                      ^
 		--library="ios-unified":"%BINARIES_DIR%\Sitecore.MobileSDK.dll"                             ^
		--library="ios-unified":"%BINARIES_DIR%\Sitecore.MobileSDK.PasswordProvider.iOS.UnifiedAPI.dll" ^
        --library="ios-unified":"%BINARIES_DIR%\Sitecore.MobileSDK.PasswordProvider.Interface.dll"  ^
		--library="ios-unified":"%BINARIES_DIR%\Microsoft.Threading.Tasks.Extensions.dll"           ^
		--library="ios-unified":"%BINARIES_DIR%\Microsoft.Threading.Tasks.dll"                      ^
		--library="ios-unified":"%BINARIES_DIR%\Newtonsoft.Json.dll"                                ^
		--library="ios-unified":"%BINARIES_DIR%\System.Net.Http.Extensions.dll"                     ^
		--library="ios-unified":"%BINARIES_DIR%\System.Net.Http.Primitives.dll"                     ^
		--library="ios-unified":"%BINARIES_DIR%\System.Net.Http.dll"                                ^
		--library="ios-unified":"%BINARIES_DIR%\System.Threading.Tasks.dll"                         ^
		--library="ios-unified":"%BINARIES_DIR%\crypto.dll"                                         ^
		--library="ios-unified":"%BINARIES_DIR%\System.IO.dll"                                      ^
		--library="android":"%BINARIES_DIR%\Sitecore.MobileSDK.dll"                         ^
		--library="android":"%BINARIES_DIR%\Sitecore.MobileSDK.PasswordProvider.Interface.dll" ^
		--library="android":"%BINARIES_DIR%\Sitecore.MobileSDK.PasswordProvider.Android.dll" ^
		--library="android":"%BINARIES_DIR%\Microsoft.Threading.Tasks.Extensions.dll"       ^
		--library="android":"%BINARIES_DIR%\Microsoft.Threading.Tasks.dll"                  ^
		--library="android":"%BINARIES_DIR%\Newtonsoft.Json.dll"                            ^
		--library="android":"%BINARIES_DIR%\System.Net.Http.Extensions.dll"                 ^
		--library="android":"%BINARIES_DIR%\System.Net.Http.Primitives.dll"                 ^
		--library="android":"%BINARIES_DIR%\System.Net.Http.dll"                            ^
		--library="android":"%BINARIES_DIR%\System.Threading.Tasks.dll"                     ^
		--library="android":"%BINARIES_DIR%\crypto.dll"                                     ^
		--library="android":"%BINARIES_DIR%\System.IO.dll"                                  ^
		--library="winphone-8.0":"%BINARIES_DIR%\Sitecore.MobileSDK.dll"                    ^
		--library="winphone-8.0":"%BINARIES_DIR%\Sitecore.MobileSDK.PasswordProvider.Interface.dll"     ^
		--library="winphone-8.0":"%BINARIES_DIR%\Microsoft.Threading.Tasks.Extensions.dll"  ^
		--library="winphone-8.0":"%BINARIES_DIR%\Microsoft.Threading.Tasks.dll"             ^
		--library="winphone-8.0":"%BINARIES_DIR%\Newtonsoft.Json.dll"                       ^
		--library="winphone-8.0":"%BINARIES_DIR%\System.Net.Http.Extensions.dll"            ^
		--library="winphone-8.0":"%BINARIES_DIR%\System.Net.Http.Primitives.dll"            ^
		--library="winphone-8.0":"%BINARIES_DIR%\System.Net.Http.dll"                       ^
		--library="winphone-8.0":"%BINARIES_DIR%\System.Threading.Tasks.dll"                ^
		--library="winphone-8.0":"%BINARIES_DIR%\crypto.dll"                                ^
		--library="winphone-8.0":"%BINARIES_DIR%\System.IO.dll"                             ^
		--sample="iOS Sample for Sitecore Mobile SDK. Downloads a single item and shows an alert with its fields.":"%REPOSITORY_ROOT%/test/LocalXamarinStoreTest/iMobileSdkDemo/iMobileSdkDemo.sln" ^
		--sample="Android Sample for Sitecore Mobile SDK. Downloads a single item and shows an alert with its fields.":"%REPOSITORY_ROOT%\test\LocalXamarinStoreTest\AndroidMobileSdkDemo\YourSolutionName.sln"
