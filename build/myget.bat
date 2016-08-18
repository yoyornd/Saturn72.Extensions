	set prjName=Saturn72.Extensions
	set slnName=Saturn72.Extensions
	
	set testPrjRegEx=*.Tests
	set testPrjBinRegEx=*test*.dll
	
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

	
	set slnPath=%srcDir%\%slnName%.sln
	set prjDir=%srcDir%\%prjCs
	set prjCs=%prjDir%\%prjName%.csproj
	set testBin=%srcDir%\Tests\%prjName%.Tests\bin\%config%\%prjName%.Tests.dll

	REM Restore nuget packages
	set pkgDir=%srcDir%\packages
	echo restore nuget packages from %srcDir% to %pkgDir% directory
	call %NuGet% restore %srcDir% -OutputDirectory %pkgDir%

	REM Build
	echo Start building %slnPath% using %MsBuildExe%
	"%MsBuildExe%" %slnPath% /p:Configuration="%config%" /m /v:M /fl /flp:LogFile=msbuild.log;Verbosity=Normal /nr:false
	
	REM Unit tests
	set nugetBuildConfig=%SourcesPath%\Build\packages.config
	echo install build nuget dependencies from %nugetBuildConfig% to %pkgDir%
	call %Nuget% install %nugetBuildConfig% -OutputDirectory %pkgDir%

	echo run unit tests from %testBin%
	echo Gather all Test Assemblies

	set curDir=%cd%
	cd %srcDir%
	FOR /D /r %%G in (%testPrjRegEx%) DO (
		Echo Scanning %%G
		cd %%G
		For /R %%F in (%testPrjBinRegEx%) do (
			echo found %%F
			Echo.%%F | findstr /i /v /C:"obj">nul && (set testDlls=%testDlls% %%F)			
		)		
	)
	
	echo VSTests assemblies list: %testDlls%		
	REM back to working dir
	cd %curDir%
	
	%VsTestConsole% %testDlls%
	if not "%errorlevel%"=="0" goto failure

	REM set nunitExe=%pkgDir%\NUnit.ConsoleRunner.3.4.1\tools\nunit3-console.exe
	REM %nunitExe% /config:%config% %testBin%
	REM if not "%errorlevel%"=="0" goto failure

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