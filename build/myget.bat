	set prjName=Saturn72.Extensions
	set slnName=Saturn72.Extensions
	
	set testPrjRegEx=*.Tests
	set testPrjBinRegEx=*.tests.dll
	
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
	set prjDir=%srcDir%\%prjName%
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

	SETLOCAL ENABLEDELAYEDEXPANSION

	FOR /D /r %%G in (%testPrjRegEx%) DO (
		Echo Scanning %%G
		cd %%G
		For /R %%F in (%testPrjBinRegEx%) do (
			echo found %%F
			Echo.%%F|findstr /r /i /v /C:"obj" >nul && (
				echo Add %%F to test assemblies collection
				set testDlls=!testDlls! %%F
				) || (
				echo Skip %%F
			)
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
	echo create Build directory (if not exists)
	mkdir Build
	echo Packging all listed projects
	
	REM - general project packing
	echo Packging %prjCs% project to Build directory
	set tmpPrjCs=%prjCs%
	call %NuGet% pack %tmpPrjCs% -symbols -o Build -p Configuration=%config% %version%
	if not "%errorlevel%"=="0" goto failure
	
	REM In case multiple packages required - explicit nuget package for each project
	set tmpPrjName=Saturn72.Extensions.AspNet
	set tmpPrjCs=%srcDir%\%tmpPrjName%\%tmpPrjName%.csproj
	call %NuGet% pack %tmpPrjCs% -symbols -o Build -p Configuration=%config% %version%
	if not "%errorlevel%"=="0" goto failure
	
	REM In case multiple packages required - explicit nuget package for each project
	set tmpPrjName=Saturn72.Extensions.Data
	set tmpPrjCs=%srcDir%\%tmpPrjName%\%tmpPrjName%.csproj
	call %NuGet% pack %tmpPrjCs% -symbols -o Build -p Configuration=%config% %version%
	if not "%errorlevel%"=="0" goto failure	
	
	REM In case multiple packages required - explicit nuget package for each project
	set tmpPrjName=Saturn72.Utils
	set tmpPrjCs=%srcDir%\%tmpPrjName%\%tmpPrjName%.csproj
	call %NuGet% pack %tmpPrjCs% -symbols -o Build -p Configuration=%config% %version%
	if not "%errorlevel%"=="0" goto failure	
	
	goto success

	:success
	exit 0

	:failure
	exit -1