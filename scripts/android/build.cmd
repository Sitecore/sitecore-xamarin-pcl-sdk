@echo off

call settings.cmd

echo Starting script...
echo Settings :
echo msbuild_path : %msbuild_path%
echo solution_path : %solution_path%
echo nuget_path : %nuget_path%

:loop
IF NOT "%1"=="" (
    IF "%1"=="-csproj" (
        SET csproj=%2
        SHIFT
    )
    IF "%1"=="-solution" (
        SET solution=%2
        SHIFT
    )
    IF "%1"=="-project_folder" (
        SET project_folder=%2
        SHIFT
    )
    IF "%1"=="-build_mode" (
        SET build_mode=%2
        SHIFT
    )
    SHIFT
    GOTO :loop
)

ECHO -----------------
ECHO Passed arguments:
ECHO csproj = %csproj%
ECHO solution = %solution%
ECHO build_mode = %build_mode%
ECHO project_folder = %project_folder%

:: Clearing folder with binaries if it exists 
if exist %project_folder%\bin\%build_mode% del %project_folder%\bin\%build_mode%\*.* /q

::Clearing project
%msbuild_path% %project_folder%\%csproj% /p:Configuration=%build_mode% /t:Clean

::Restoring solution packages
%nuget_path% restore %solution_path%\%solution%

::Building project and creating apk.
%msbuild_path% %project_folder%\%csproj% /p:Configuration=%build_mode% /t:SignAndroidPackage