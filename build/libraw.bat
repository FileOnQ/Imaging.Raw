@echo off
title Building LibRaw
set arch=%1
set vcvar=
set progFile=


::check default 
set vswhereFile="%ProgramFiles%\Microsoft Visual Studio\Installer\vswhere.exe"
if exist %vswhereFile% (	
	goto setprogfile
)
goto check86



:setprogfile
set progFile=%ProgramFiles%
goto vswhereFound



:check86
set vswhereFile86="%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe"
if exist "%vswhereFile86%" (
	set progFile=%vswhereFile86%
	goto vswhereFound
)
goto fileNotFound



:vswhereFound
set vcvar=
for /f "usebackq tokens=1 delims=" %%x in (`%progFile% -find **\vcvarsall.bat`) do set vcvar="%%~x"
if defined vcvar goto fileFound



:fileNotFound
echo "unable to find 'vcvarsall.bat'. visual studio c++ package needs to be installed"
exit /b 2



:fileFound
echo "vcvarsall.bat was found, continuing"

call %vcvar% %arch%
cd ../LibRaw

git clean -xd -f lib -f bin -f object

if "%arch%" == "x64" (
	nmake -f makefile_x64.msvc
)

if "%arch%" == "x86" (
	nmake -f makefile_x86.msvc
)

cd ../build
