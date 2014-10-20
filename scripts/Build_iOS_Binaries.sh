LAUNCH_DIR=$PWD
SCRIPTS_DIR=$LAUNCH_DIR

cd ..
REPOSITORY_ROOT=$PWD
cd "$LAUNCH_DIR"


DEPLOYMENT_DIR="$REPOSITORY_ROOT/deployment"
SOLUTIONS_DIR="$REPOSITORY_ROOT/solutions"
MDTOOL_EXE="/Applications/Xamarin Studio.app/Contents/MacOS/mdtool"


echo "===========Environment==========="
echo "LAUNCH_DIR - $LAUNCH_DIR"
echo "REPOSITORY_ROOT - $REPOSITORY_ROOT"
echo "SCRIPTS_DIR - $SCRIPTS_DIR"
echo "SOLUTIONS_DIR - $SOLUTIONS_DIR"
echo "MDTOOL_EXE - $MDTOOL_EXE"


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

    echo "===========Build_Tests==========="
	"$MDTOOL_EXE" --verbose build "--configuration:Release|iPhone" MobileSDKTest-iOS.sln


    echo "===========NuGet==========="
	## White label iOS app
	rm -rf "$PWD/packages"
	nuget restore WhiteLabel-iOS.sln

    echo "===========Build_DemoApp==========="
	"$MDTOOL_EXE" --verbose build "--configuration:Release|iPhone" WhiteLabel-iOS.sln
cd "$LAUNCH_DIR"


echo "===========IPA==========="
## create *.ipa
xcrun -sdk iphoneos \
   PackageApplication "$REPOSITORY_ROOT/test/iOS/MobileSDKUnitTest-iOS/bin/iPhone/Release/MobileSDKUnitTestiOS.app" \
   -o "$DEPLOYMENT_DIR/MobileSDKUnitTestiOS.ipa"

xcrun -sdk iphoneos \
   PackageApplication "$REPOSITORY_ROOT/test/iOS/MobileSDK-IntegrationTest-iOS/bin/iPhone/Release/MobileSDKIntegrationTestiOS.app" \
   -o "$DEPLOYMENT_DIR/MobileSDKIntegrationTestiOS.ipa"

xcrun -sdk iphoneos \
   PackageApplication "$REPOSITORY_ROOT/app/WhiteLabel/iOS/WhiteLabel-iOS/bin/iPhone/Release/WhiteLabeliOS.app" \
   -o "$DEPLOYMENT_DIR/WhiteLabeliOS.ipa"



echo "===========DSYM==========="
## copy *.dsym
cd "$REPOSITORY_ROOT/test/iOS/MobileSDKUnitTest-iOS/bin/iPhone/Release/"
zip -r MobileSDKUnitTestiOS.app.dSYM.zip MobileSDKUnitTestiOS.app.dSYM
cp MobileSDKUnitTestiOS.app.dSYM.zip "$DEPLOYMENT_DIR"


cd "$REPOSITORY_ROOT/test/iOS/MobileSDK-IntegrationTest-iOS/bin/iPhone/Release/"
zip -r MobileSDKIntegrationTestiOS.app.dSYM.zip MobileSDKIntegrationTestiOS.app.dSYM
cp MobileSDKIntegrationTestiOS.app.dSYM.zip "$DEPLOYMENT_DIR"

cd "$REPOSITORY_ROOT/app/WhiteLabel/iOS/WhiteLabel-iOS/bin/iPhone/Release/"
zip -r WhiteLabeliOS.app.dSYM.zip WhiteLabeliOS.app.dSYM
cp WhiteLabeliOS.app.dSYM.zip "$DEPLOYMENT_DIR"




echo "===========Testflight==========="
## Upload to Testflight
cd "$DEPLOYMENT_DIR"
    curl http://testflightapp.com/api/builds.json \
    -F file=@MobileSDKUnitTestiOS.ipa \
    -F api_token='a3620a9760dc97411328bc73864c9a82_Mzk2NDIxMjAxMi0wNC0xMyAwNDowNzozMS42MDQ2NTQ' \
    -F team_token='9a4cf26c2ba6dab0ae956567f92fc0c0_ODA1NzEyMDEyLTA0LTEzIDA0OjEzOjM5LjA5OTQ0Nw' \
    -F notes='Built by Hudson' \
    -F notify=True \
    -F distribution_lists='Mobile iOS Team' \
    -F dsym=@MobileSDKUnitTestiOS.app.dSYM.zip


    curl http://testflightapp.com/api/builds.json \
    -F file=@MobileSDKIntegrationTestiOS.ipa \
    -F api_token='a3620a9760dc97411328bc73864c9a82_Mzk2NDIxMjAxMi0wNC0xMyAwNDowNzozMS42MDQ2NTQ' \
    -F team_token='9a4cf26c2ba6dab0ae956567f92fc0c0_ODA1NzEyMDEyLTA0LTEzIDA0OjEzOjM5LjA5OTQ0Nw' \
    -F notes='Built by Hudson' \
    -F notify=True \
    -F distribution_lists='Mobile iOS Team' \
    -F dsym=@MobileSDKIntegrationTestiOS.app.dSYM.zip


    curl http://testflightapp.com/api/builds.json \
    -F file=@WhiteLabeliOS.ipa \
    -F api_token='a3620a9760dc97411328bc73864c9a82_Mzk2NDIxMjAxMi0wNC0xMyAwNDowNzozMS42MDQ2NTQ' \
    -F team_token='9a4cf26c2ba6dab0ae956567f92fc0c0_ODA1NzEyMDEyLTA0LTEzIDA0OjEzOjM5LjA5OTQ0Nw' \
    -F notes='Built by Hudson' \
    -F notify=True \
    -F distribution_lists='Mobile iOS Team' \
    -F dsym=@WhiteLabeliOS.app.dSYM.zip
