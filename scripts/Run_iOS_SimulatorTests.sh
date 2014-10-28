LAUNCH_DIR=$PWD
SCRIPTS_DIR=$LAUNCH_DIR

cd ..
REPOSITORY_ROOT=$PWD
cd "$LAUNCH_DIR"


DEPLOYMENT_DIR="$REPOSITORY_ROOT/deployment"
SOLUTIONS_DIR="$REPOSITORY_ROOT/solutions"


INTEGRATION_TEST_APP="$REPOSITORY_ROOT/test/iOS/MobileSDK-IntegrationTest-iOS/bin/iPhoneSimulator/Release/MobileSDKIntegrationTestiOS.app"
UNIT_TEST_APP="$REPOSITORY_ROOT/test/iOS/MobileSDKUnitTest-iOS/bin/iPhoneSimulator/Release/MobileSDKUnitTestiOS.app"


TEST_REPORT_RECEIVER_EXE="$SCRIPTS_DIR/Touch.Server.exe"
MDTOOL_EXE="/Applications/Xamarin Studio.app/Contents/MacOS/mdtool"
MONO_EXE=mono


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
	"$MDTOOL_EXE" --verbose build "--configuration:Release|iPhoneSimulator" MobileSDKTest-iOS.sln
cd "$LAUNCH_DIR"



# echo "===========Run_Unit_Tests==========="
# "$MONO_EXE" "$TEST_REPORT_RECEIVER_EXE" \
#   --autoexit                            \
#   --port=16390                          \
#   --launchsim="$UNIT_TEST_APP"          \
#   --logfile="$DEPLOYMENT_DIR/UnitTestReport.xml"



echo "===========Run_Integraation_Tests==========="
"$MONO_EXE" "$TEST_REPORT_RECEIVER_EXE" \
  --autoexit                            \
  --port=16391                          \
  --launchsim="$INTEGRATION_TEST_APP"   \
  --logfile="$DEPLOYMENT_DIR/IntegrationTestReport.xml"












