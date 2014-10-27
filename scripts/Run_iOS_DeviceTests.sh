LAUNCH_DIR=$PWD
SCRIPTS_DIR=$LAUNCH_DIR

cd ..
REPOSITORY_ROOT=$PWD
cd "$LAUNCH_DIR"


DEPLOYMENT_DIR="$REPOSITORY_ROOT/deployment"
SOLUTIONS_DIR="$REPOSITORY_ROOT/solutions"


INTEGRATION_TEST_APP="$REPOSITORY_ROOT/test/iOS/MobileSDK-IntegrationTest-iOS/bin/iPhone/Release/MobileSDKIntegrationTestiOS.app"
INTEGRATION_TEST_APP_BUNDLE_ID="net.sitecore.MobileSDKIntegrationTestiOS"

UNIT_TEST_APP="$REPOSITORY_ROOT/test/iOS/MobileSDKUnitTest-iOS/bin/iPhone/Release/MobileSDKUnitTestiOS.app"
UNIT_TEST_APP_BUNDLE_ID="net.sitecore.MobileSDKUnitTestiOS"


TEST_REPORT_RECEIVER_EXE="$SCRIPTS_DIR/Touch.Server.exe"
MDTOOL_EXE="/Applications/Xamarin Studio.app/Contents/MacOS/mdtool"
MONO_EXE=mono
MTOUCH_EXE="/Developer/MonoTouch/usr/bin/mtouch"




echo "===========Environment==========="
echo "LAUNCH_DIR - $LAUNCH_DIR"
echo "REPOSITORY_ROOT - $REPOSITORY_ROOT"
echo "SCRIPTS_DIR - $SCRIPTS_DIR"
echo "SOLUTIONS_DIR - $SOLUTIONS_DIR"


echo "INTEGRATION_TEST_APP - $INTEGRATION_TEST_APP"
echo "UNIT_TEST_APP - $UNIT_TEST_APP"


echo "TEST_REPORT_RECEIVER_EXE - $TEST_REPORT_RECEIVER_EXE"
echo "MDTOOL_EXE - $MDTOOL_EXE"
echo "MONO_EXE - $MONO_EXE"
echo "MTOUCH_EXE - $MTOUCH_EXE"



echo "===========Clean==========="
rm -rf "$DEPLOYMENT_DIR"
mkdir -p "$DEPLOYMENT_DIR"



## Clean temporary build data
cd "$REPOSITORY_ROOT"
find . \( -name "bin" -o  -name "obj" \)  -exec rm -rf {} \;




cd "$SOLUTIONS_DIR"
	
  echo "===========NuGet==========="
  ## Unit test and integration test
	rm -rf "$PWD/packages"
	nuget restore MobileSDKTest-iOS.sln


  echo "===========Build==========="  
	"$MDTOOL_EXE" --verbose build "--configuration:Release|iPhone" MobileSDKTest-iOS.sln
cd "$LAUNCH_DIR"



echo "===========Run_Unit_Tests==========="
DEVICE_NAME="1"

"$MTOUCH_EXE" \
  --devname="$DEVICE_NAME" \
  --installdev "$UNIT_TEST_APP"

"$MTOUCH_EXE" \
  --devname="$DEVICE_NAME" \
  --killdev "$UNIT_TEST_APP_BUNDLE_ID"


"$MONO_EXE" "$TEST_REPORT_RECEIVER_EXE"   \
  --autoexit                              \
  --port=16390                            \
  --launchdev="$UNIT_TEST_APP_BUNDLE_ID"  \
  --devname="$DEVICE_NAME"                \
  --logfile="$DEPLOYMENT_DIR/UnitTestReport-Device.xml"



echo "===========Run_Integraation_Tests==========="
"$MTOUCH_EXE" \
  --devname="$DEVICE_NAME" \
  --installdev "$INTEGRATION_TEST_APP"

"$MTOUCH_EXE" \
  --devname="$DEVICE_NAME" \
  --killdev "$INTEGRATION_TEST_APP_BUNDLE_ID"


"$MONO_EXE" "$TEST_REPORT_RECEIVER_EXE"       \
  --autoexit                                   \
  --port=16391                                  \
  --launchdev="$INTEGRATION_TEST_APP_BUNDLE_ID"  \
  --devname="$DEVICE_NAME"                        \
  --logfile="$DEPLOYMENT_DIR/IntegrationTestReport-Device.xml"












