mono Touch.Server.exe \
  --verbose           \
  --autoexit          \
  --port=16390        \
  --launchsim="/Volumes/untitled/tfs/Mobile.SDK.dotNET/test/iOS/MobileSDKUnitTest-iOS/bin/iPhoneSimulator/Debug/MobileSDKUnitTestiOS.app" \
  --logfile=UnitTestReport.xml
##############


##devname does not work properly for Touch.Server.exe


/Developer/MonoTouch/usr/bin/mtouch \
  --devname="2" \
  --installdev "/Volumes/untitled/tfs/Mobile.SDK.dotNET/test/iOS/MobileSDKUnitTest-iOS/bin/iPhone/Debug/MobileSDKUnitTestiOS.app" 


/Developer/MonoTouch/usr/bin/mtouch \
  --devname="2" \
  --killdev "net.sitecore.MobileSDKUnitTestiOS"

mono Touch.Server.exe \
  --verbose           \
  --autoexit          \
  --port=16394        \
  --launchdev="net.sitecore.MobileSDKUnitTestiOS" \
  --devname="2" \
  --logfile=UnitTestReport-Device-iOS8.xml
##############


/Developer/MonoTouch/usr/bin/mtouch \
  --devname="1" \
  --installdev "/Volumes/untitled/tfs/Mobile.SDK.dotNET/test/iOS/MobileSDKUnitTest-iOS/bin/iPhone/Debug/MobileSDKUnitTestiOS.app" 


/Developer/MonoTouch/usr/bin/mtouch \
  --devname="1" \
  --killdev "net.sitecore.MobileSDKUnitTestiOS"

mono Touch.Server.exe \
  --verbose           \
  --autoexit          \
  --port=16394        \
  --launchdev="net.sitecore.MobileSDKUnitTestiOS" \
  --devname="1" \
  --logfile=UnitTestReport-Device.xml
##############
