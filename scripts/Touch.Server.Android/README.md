**Touch Server For Android**
==================

The project is designed to facilitate running **Xamarin Android NUnitLite** tests on CI server.

This server listens for connection from device and after collects test results and saves to file. Additionally it can launch application after server started and  wait till all tests are finished.

This project is based on [Touch Server for IOS](https://github.com/spouliot/Touch.Unit/tree/master/Touch.Server "Touch Server for IOS"). 

**Features**

 1. Collect test results from device.
 2. Auto exit after connection from device is closed.
 3. Save received results to file.
 4. Launch android application after server started and wait for test results from device.

**Command line options** 

      --ip          IP address to listen (default: 0.0.0.0)
    
      --port        TCP port to listen (default: Any)
    
      --logpath     Path to save the log files (default: .)
    
      --logfile     File name to save the log to (default: automatically generated)
    
      --autoexit    (Default: False) Exit the server once a test run has completed 
                    (default: false)
    
      --activity    Fully qualified activity name that will be launched after 
                    server starts.(exmaple : package_name/namespace.MainActivity)
    
      --adbpath     Path to adb.exe (default: will use adb.exe from environment)
    
      -v            Print details during execution.
    
      --help        Display this help screen.

**Examples**
------------------------

    Touch.Server.Android.exe --ip=192.168.0.1 --port=8888 --autoexit --logfile="results.txt"


    Touch.Server.Android.exe --ip=192.168.0.1 --port=8888 --autoexit --activity="Your.Package.Name/Your.Namespace.MainActivity" --logfile="results.xml" --adbpath="your\android\sdk\location\platform-tools"