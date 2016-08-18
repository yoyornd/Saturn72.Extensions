@echo Off

set config=%1
if "%config%" == "" (
   set config=Release
)

set version=
if not "%PackageVersion%" == "" (
   set version=-Version %PackageVersion%
)

set srcDir=%SourcesPath%\src
set pkgDir=%srcDir%\packages

set prjName = Saturn72.UnitTesting.Framework
set slnName=%SourcesPath%\**\*.sln
set prjDir=%srcDir%\%prjCs
set prjCs=%prjDir%\%prjName%.csproj
set testBin=%srcDir%\Tests\%prjName%.Tests\bin\%config%\%prjName%.Tests.dll

REM Restore nuget packages
set pkgDir=%cd%\%srcDir%\packages
echo restore nuget packages to %pkgDir% directory
call %NuGet% restore %slnName% -OutputDirectory %pkgDir% -NonInteractive

REM Build
echo Start building %slnName% using %MsBuildExe%
%MsBuildExe% %slnName% /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false

REM Unit tests
echo install build nuget dependencies to %pkgDir%
call %Nuget% install %SourcesPath%\**\packages.config -OutputDirectory %pkgDir%

echo run unit tests from %testBin%
%VsTestConsole% /testcontainer:%testBin%

set nunitExe=%pkgDir%\NUnit.ConsoleRunner.3.4.1\tools\nunit3-console.exe
%nunitExe% /config:%config% %testBin%
if not "%errorlevel%"=="0" goto failure

REM Package
mkdir Build
echo Packging %prjCs% project to Build directory
call %NuGet% pack %prjCs% -symbols -o Build -p Configuration=%config% %version%
if not "%errorlevel%"=="0" goto failure

goto success

:success
exit 0

:failure
exit -1