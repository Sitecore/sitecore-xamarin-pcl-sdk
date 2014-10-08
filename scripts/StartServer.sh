mono Touch.Server.exe \
  --verbose           \
  --autoexit          \
  --port=16384        \
  --launchsim="/Volumes/untitled/tfs/Mobile.SDK.dotNET/test/iOS/MobileSDKUnitTest-iOS/bin/iPhoneSimulator/Debug/MobileSDKUnitTestiOS.app" \
  --logfile=UnitTestReport.xml
##############


/Developer/MonoTouch/usr/bin/mtouch --installdev "/Volumes/untitled/tfs/Mobile.SDK.dotNET/test/iOS/MobileSDKUnitTest-iOS/bin/iPhone/Debug/MobileSDKUnitTestiOS.app"
/Developer/MonoTouch/usr/bin/mtouch --killdev "net.sitecore.MobileSDKUnitTestiOS"

mono Touch.Server.exe \
  --verbose           \
  --autoexit          \
  --port=16388        \
  --launchdev="net.sitecore.MobileSDKUnitTestiOS" \
  --logfile=UnitTestReport-Device.xml


  --device=""

https://evolve.xamarin.com/live
