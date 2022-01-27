::@echo off
title Building LibRaw
set arch=%1
set vcvar=

set progFiles="%ProgramFiles%\Microsoft Visual Studio\Installer\vswhere.exe"
for /f "usebackq tokens=1 delims=" %%x in (`"%progFiles%" -find **\vcvarsall.bat`) do set vcvar="%%~x"

if defined vcvar goto fileFound


if not defined ProgramFiles(x86) goto fileNotFound


for /f "usebackq tokens=1 delims=" %%y in (`"%ProgramFiles(x86)%\Microsoft Visual Studio\Installer\vswhere.exe" -find **\vcvarsall.bat`) do set vcvar="%%~y"
if defined vcvar goto fileFound



:fileNotFound
echo "unable to find 'vcvarsall.bat'. visual studio c++ package needs to be installed"
exit /b 2



:fileFound
echo "csvarsall.bat was found, continuing"

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
